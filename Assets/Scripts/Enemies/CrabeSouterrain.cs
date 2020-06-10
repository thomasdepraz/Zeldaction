using Cinemachine;
using System.Collections;
using UnityEngine;

public class CrabeSouterrain : MonoBehaviour
{
    // Variables
    private Rigidbody2D rb;
    private Vector2 direction;

    private GameObject player;
    public GameObject baseCrab;
    public GameObject particles;
    public GameObject loot;
    private PlayerHP playerHP;
    private Hookable hookable;
    private float distanceToPlayer;
    public CinemachineImpulseSource impulseSource;
    public UndergroundCrabAudioManager audioManager;

    
    [Header("Tweaks")]
    [Range(0f, 100F)]
    public float speed;
    [Range(0, 7)]
    public int attackDamage = 2;
    [Range(0f, 5f)]
    public float detectionDistance;
    [Range(0f, 5f)]
    public float attackDistance;

    public GameObject pinceCrabe;
    private bool isDead;

    [Header("LoadTime")]
    public float loadAttack;
    public float loadMovement;
    public float stunTime = 5f;

    bool canGiveDamage = false;
    bool canMove = true;
    bool canStartCoroutine;

    private Animator anim;
    private void OnEnable()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerHP = player.GetComponent<PlayerHP>();
    }
    void Start()
    {
        canStartCoroutine = true;
        rb = gameObject.GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        playerHP = player.GetComponent<PlayerHP>();
        hookable = gameObject.GetComponent<Hookable>();
        anim = gameObject.GetComponent<Animator>();
    }

    void Update()
    { 
        if (!isDead)
        {
            CrabeAttack();
            Movement();
        }
    }

    // Déplacement du crabe : Si la distance entre le PJ et le crabe est inférieur à la distance de détection, le crabe se met en mouvement.     
    private void Movement()
    {
        distanceToPlayer = (player.transform.position - gameObject.transform.position).magnitude;

        if(distanceToPlayer <= detectionDistance && canMove)//condition de déplacement
        {
            direction = player.transform.position - gameObject.transform.position;
            rb.velocity = direction.normalized * speed * Time.deltaTime;
            particles.SetActive(true);

            if(!audioManager.soundSource.isPlaying)
            {
                if (audioManager.soundSource.clip != audioManager.move)
                {
                    audioManager.PlayAndLoop(audioManager.move, 1, audioManager.output);
                }
            }
        }
        else
        {
            rb.velocity = Vector2.zero;
            particles.SetActive(false);
        }
    }

    private void CrabeAttack() // Fonction de déclenchement de l'attaque 
    {
        if(distanceToPlayer <= attackDistance)//Condition au déclenchement
        {
            rb.velocity = Vector2.zero;
            canMove = false;
            canGiveDamage = true;
            if(canStartCoroutine)
            {
                StartCoroutine(LoadAttack());
            }
        }
        else //Annulation du déclenchement
        {            
            canGiveDamage = false;
        }

    }

    IEnumerator LoadAttack() 
    {
        canStartCoroutine = false;//on évite que la coroutine se lance 100 fois

        yield return new WaitForSeconds(loadAttack); //préparation de l'attaque
        hookable.isActive = true;
        audioManager.PlayClip(audioManager.clawOut, 1, audioManager.output);
        anim.SetBool("isAttacking", true);                         

        yield return new WaitForSeconds(stunTime);//après attaque stun puis peut bouger à nouveau
        anim.SetBool("isRetracting", true);
        audioManager.PlayClip(audioManager.clawIn, 1, audioManager.output);
        hookable.isActive = false;   
    }

    // Il manque l'élimination du crabe qui s'effectue avec le pull du crabe via l'hameçon du PJ.
    public void isPulled()
    {
        hookable.isActive = false;
        anim.SetBool("isDead", true);
        audioManager.PlayClip(audioManager.death, 1, audioManager.output);
    }

    public void GetAnimationEvent(string parameter)
    {
        if(parameter == "attack")
        {
            if (canGiveDamage == true)//si player en range alors dégats
            {
                impulseSource.GenerateImpulse(Vector3.up);
                playerHP.TakeDamage(attackDamage);
            }
        }
        if (parameter == "attackEnded")
        {
            anim.SetBool("isAttacking", false);
        }

        if(parameter == "retractEnded")
        {
            anim.SetBool("isRetracting", false);
            canMove = true;
            canStartCoroutine = true;
        }

        if(parameter == "deathEnded")
        {
            float stat = Random.Range(0, 1);
            if(stat < 0.6)
            {
                GameObject.Instantiate(loot, gameObject.transform.position, Quaternion.identity);
            }
            isDead = true;
            GetComponent<BoxCollider2D>().enabled = false;
            rb.simulated = false;
            gameObject.transform.parent = null;
        }
    }
}

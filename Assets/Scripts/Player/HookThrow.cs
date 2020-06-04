using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Player;
using UnityEngine.UI;
using System.Runtime.CompilerServices;

public class HookThrow : MonoBehaviour
{

    [Header("Elements")]
    private GameObject player;
    public GameObject hook;
    private SpriteRenderer hookSpr;
    private Rigidbody2D hookRigidBody;
    public GameObject crosshair;
    private PlayerMovement playerMovement;
    private PlayerAim playerAim;
    public Sprite hookAvailable;
    public Sprite hookUnavailable;
    public Image hookUI;
    public LineRenderer hookLine;
    public BoxCollider2D playerHitBox;
    public PlayerAudioManager playerAudio;
    public ParticleSystem hookHitParticle;

    [Header("Logic")]
    [Range(0f, 10f)]
    public float speed;
    private Vector3 direction;
    [HideInInspector] public bool isThrown = false;
    [HideInInspector]public bool isHooked = false;
    [HideInInspector]public bool isPulling = false;
    private bool canStartCoroutine = true;
    private bool canStartCoroutineHitbox = true;
    private ContactFilter2D hookableFilter;
    private float objectDrag;
    private ContactPoint2D[] hookContacts = new ContactPoint2D[10];
    private ContactPoint2D[] playerContacts = new ContactPoint2D[10];
    private bool canStartUnhook = true;
    private bool canHook = true;

    [Header("Tweak")]
    [Range(0f, 5f)]
    public float hookDetectionRange;
    [Range(0f, 5f)]
    public float timeToCancel = 3f;
    [Range(0f, 15f)]
    public float hookRange;

    private Rigidbody2D playerRb;


    // Start is called before the first frame update
    void Start()
    {
        player = gameObject;
        hookRigidBody = hook.GetComponent<Rigidbody2D>();
        playerMovement = player.GetComponent<PlayerMovement>();
        playerAim = player.GetComponent<PlayerAim>();
        playerRb = player.GetComponent<Rigidbody2D>();
        hookSpr = hook.GetComponent<SpriteRenderer>();

        hookableFilter.useTriggers = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(!hookUI.gameObject.activeSelf)
        {
            if(!PlayerManager.hasHook)
            {
                hookUI.gameObject.SetActive(false);
            }
            else
            {
                hookUI.gameObject.SetActive(true);
            }
        }

        if(PlayerManager.hasHook && PlayerManager.useHook)
        {
            if (Input.GetButtonDown("Throw") && !isThrown && !isHooked && playerAim.isAiming)//Si le hameçon n'est pas lancé et qu'on appui sur R1 alors on le lance.
            {
                hook.GetComponent<BoxCollider2D>().isTrigger = false;
                hookRigidBody.simulated = true;
                Throw();
            }
            else if (Input.GetButtonDown("Throw") && isThrown && !isPulling)// si l'hameçon est lancée et qu'on appuie sur R1 on le tire
            {
                Pull();                
            }
            if (isThrown) //Quand le hameçon est lancé on vérifie s'il peut se hook
            {
                Hook();
                hookLine.SetPosition(0, transform.position);
                hookLine.SetPosition(1, hook.transform.position);
            }

            if((player.transform.position - hook.transform.position).sqrMagnitude > hookRange && isThrown)
            {
                Pull();
            }

            if(playerAim.isAiming)
            {
                hookSpr.enabled = true;
            }
            else if(!playerAim.isAiming)
            {
                hookSpr.enabled = false;
            }

            if(isThrown && !isPulling)
            {
                if(hookRigidBody.GetContacts(hookContacts) >= 1 && hook.transform.parent == gameObject.transform)
                {
                    hookHitParticle.Play();
                    Pull();
                }
                
               
            }

            if(isPulling)
            {
                hook.GetComponent<BoxCollider2D>().isTrigger = true;

                if(hook.transform.parent != gameObject.transform)
                {
                    if(canStartUnhook)
                    {
                        StartCoroutine("UnHook");
                    }
                }
            }
        }
    }
    Vector2 storedVelocity;
    void Throw()
    {
        Debug.Log("Throwing");
        direction = (crosshair.transform.position - hook.transform.position).normalized; //direction player-> crosshair
        hookRigidBody.velocity += (Vector2)direction * speed; //déplacement du hook
        storedVelocity = hookRigidBody.velocity;
        isThrown = true;
        hookUI.sprite = hookUnavailable;
        playerAudio.PlayClip(playerAudio.hookSoundSource, playerAudio.hookThrow, 1, playerAudio.hook);
    }

    public void Pull()
    {
        isPulling = true;
        playerMovement.canMove = false;
        playerMovement.playerRb.velocity = Vector2.zero;
        if (playerRb.GetContacts(playerContacts) == 0)
        {
            Physics2D.IgnoreLayerCollision(10, 10, true);
        }
        if (isHooked && hook.transform.parent.GetComponent<Hookable>().isActive)
        {
            if(hook.transform.parent.GetComponent<Hookable>().isLight)//si le truc est léger
            {
                hookRigidBody.simulated = false;
                hook.transform.parent.GetComponent<Rigidbody2D>().drag = 0f;
                direction = (transform.position - hook.transform.position);
                hook.transform.parent.GetComponent<Rigidbody2D>().velocity = direction.normalized * speed * 2;
            }
            else if(hook.transform.parent.GetComponent<Hookable>().isHeavy)//si le truc est lourd
            {
                Physics2D.IgnoreLayerCollision(10, 14, true);
                direction = (hook.transform.position - transform.position);
                playerRb.velocity = direction.normalized * speed * 2;
            }
            else if (hook.transform.parent.GetComponent<Hookable>().isUndergroundCrab)//si le truc est un crab soutterain
            {
                hook.transform.parent.GetComponent<CrabeSouterrain>().isPulled();
                hook.transform.SetParent(gameObject.transform);
                hookRigidBody.simulated = true;

                direction = (transform.position - hook.transform.position);
                hookRigidBody.velocity = Vector2.zero;
                hookRigidBody.velocity = direction.normalized * speed * 2;
            }
            else if(hook.transform.parent.GetComponent<Hookable>().isArmoredCrab)//si c'est un crabe lourd
            {
                GameObject ArmoredCrab = hook.transform.parent.gameObject;
                ArmoredCrab.GetComponent<ArmoredCrab>().anim.SetBool("isDead", true);
                ArmoredCrab.GetComponent<ArmoredCrab>().audioManager.PlayClip(ArmoredCrab.GetComponent<ArmoredCrab>().audioManager.armorDown, 1, ArmoredCrab.GetComponent<ArmoredCrab>().audioManager.output);  
                GameObject Armor = Instantiate(ArmoredCrab.GetComponent<ArmoredCrab>().armor, ArmoredCrab.transform.position, Quaternion.identity);
                hook.transform.SetParent(Armor.transform);
                objectDrag = Armor.GetComponent<Rigidbody2D>().drag;
                Pull();
            }
        }
        else //Retour de l'hameçon
        {
            direction = (transform.position - hook.transform.position);
            hookRigidBody.velocity = Vector2.zero;
            hookRigidBody.velocity = direction.normalized * speed * 2;
            playerAudio.PlayClipNat(playerAudio.hookSoundSource, playerAudio.hookPull, 1, playerAudio.hook);
        }
    }

    void Hook()
    {
        if(canHook)
        {
            List<Collider2D> hitHookables = new List<Collider2D>();
            Physics2D.OverlapCollider(hook.GetComponent<BoxCollider2D>(),hookableFilter, hitHookables);
            foreach (Collider2D hookable in hitHookables)
            {
                if(hookable.gameObject.CompareTag("Hookable") && !isHooked && isThrown)
                {
                    if(hookable.gameObject.GetComponent<Hookable>().isActive)
                    {
                        //S'accrocher à l'objet
                        playerAudio.PlayClip(playerAudio.hookSoundSource, playerAudio.onHook, 1, playerAudio.hook);
                        hook.transform.position = hookable.gameObject.transform.position;
                        hook.transform.SetParent(hookable.gameObject.transform);
                        hookRigidBody.velocity = Vector2.zero;
                        hookRigidBody.simulated = false;
                        isHooked = true;
                        objectDrag = hook.transform.parent.GetComponent<Rigidbody2D>().drag;

                        if (isPulling)
                            Pull();

                        if (canStartCoroutine)
                            StartCoroutine("HookCancel");
                    }  
                }
            }
        }

        if(isPulling &&  (player.transform.position - hook.transform.position).magnitude < hookDetectionRange)
        {
            if (isHooked)
            {
                hook.transform.parent.GetComponent<Rigidbody2D>().drag = objectDrag; //FAIRE AUTREMENT POUR LE DRAG
                hook.transform.SetParent(gameObject.transform);
                isHooked = false;
                Physics2D.IgnoreLayerCollision(10, 14, false);
            }

            playerRb.velocity = Vector2.zero;
            hookRigidBody.velocity = Vector2.zero;
            hookRigidBody.simulated = false;
            hook.transform.position = gameObject.transform.position;//POUR L'INSTANT, après la MAIN
            if(canStartCoroutineHitbox)
            {
                StartCoroutine("HitBoxOff");
            }
            isThrown = false;
            isPulling = false;
            canHook = true;
            hookUI.sprite = hookAvailable;
        }
    }

    private IEnumerator HookCancel()
    {
        canStartCoroutine = false;
        yield return new WaitForSeconds(timeToCancel);
        if(!isPulling && !hook.transform.parent.CompareTag("Player"))
        {
            isPulling = true;
            playerMovement.canMove = false;
            playerMovement.playerRb.velocity = Vector2.zero;
            hook.transform.SetParent(gameObject.transform);
            hookRigidBody.simulated = true;

            direction = (transform.position - hook.transform.position);
            hookRigidBody.velocity = Vector2.zero;
            hookRigidBody.velocity = direction.normalized * speed * 2;
            playerAudio.PlayClipNat(playerAudio.hookSoundSource,playerAudio.hookPull, 1, playerAudio.hook);
        }
        canStartCoroutine = true;
    }

    private IEnumerator UnHook()
    {
        canStartUnhook = false;
        yield return new WaitForSeconds(2f);
        if (isThrown && hook.transform.parent != gameObject.transform && isPulling)
        {
            hook.transform.parent.GetComponent<Rigidbody2D>().drag = objectDrag;
            canHook = false;
            isHooked = false;
            hook.transform.parent = gameObject.transform;
            hookRigidBody.simulated = true;
            Pull();
        }
        canStartUnhook = true;
    }

    private IEnumerator HitBoxOff()
    {
        canStartCoroutineHitbox = false;
        yield return new WaitForSeconds(0.3f);
        Physics2D.IgnoreLayerCollision(10, 10, false);
        playerMovement.canMove = true;
        canStartCoroutineHitbox = true;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(hook.transform.position, hookDetectionRange);
    }

    public void ResetHook()
    {
        hook.transform.position = player.transform.position;
        hook.transform.parent = player.transform;
        hookRigidBody.velocity = Vector2.zero;
        hookRigidBody.simulated = false;
        isHooked = false;
        isPulling = false;
        isThrown = false;
        canHook = true;
        hookUI.sprite = hookAvailable;
        PlayerManager.useHook = true;
        PlayerManager.hasHook = true;
    }
}

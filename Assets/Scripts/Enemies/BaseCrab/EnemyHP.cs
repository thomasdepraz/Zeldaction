using UnityEngine;
using System.Collections;
using Cinemachine;

public class EnemyHP : MonoBehaviour
{
    public int maxHealth = 3;
    public int currentHealth;
    private Animator anim;
    private bool crabcd;
    public GameObject loot;

    public Material damageMaterial;
    private Material defaultMaterial;

    public ParticleSystem hitParticle;
    public CinemachineImpulseSource impulseSource;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        anim = gameObject.GetComponent<Animator>();
        crabcd = true;
        defaultMaterial = GetComponent<SpriteRenderer>().material;
    }

    private void Update()
    {
 
    }

    public void TakeDamage(int damage)
    {
        hitParticle.Play();
        impulseSource.GenerateImpulse(Vector3.up);
        currentHealth -= damage; // le montant des dommages va être soustrait à la vie actuelle de l'ennemi
        StartCoroutine(DamageFB());
        if (currentHealth <= 0)
        {
            anim.SetBool("isDead", true);
        }
    }

    public void DeathAnim()
    {
        GetComponent<Collider2D>().enabled = false; // en cas de mort, le collider du monstre est désactivé
        this.enabled = false;
        GetComponent<EnemyMovement>().enabled = false;
        GetComponent<EnemyAttack>().enabled = false;

        if (BossSummoning.Instance != null)
        {
            if (crabcd == true)
            {
                StartCoroutine(CrabcounterCD());
                BossSummoning.Instance.crabcounter--; //sert à compter le nombre de crabes invoqués par le boss et à reset le time de spawn, si j'ai du utilisé Gameobject.Find [...]
                BossSummoning.Instance.startTimer = 0f;// [...] c'est pcq je ne savais pas comment lier les variables déclarées en public avec un objet qui n'est pas dans la scène. je me tate à mettre ces deux lignes dans un if.
            }

        }
        float stat = Random.Range(0f, 1f);
        if (stat < 0.3f)
        {
            GameObject.Instantiate(loot, gameObject.transform.position, Quaternion.identity);
        }
        Destroy(gameObject);
    }

    IEnumerator CrabcounterCD()
    {
        crabcd = false;
        yield return new WaitForSeconds(0.2f);
        crabcd = true;
    }
    IEnumerator DamageFB()
    {
        GetComponent<SpriteRenderer>().material = damageMaterial;
        yield return new WaitForSeconds(0.2f);
        GetComponent<SpriteRenderer>().material = defaultMaterial;
    }
}

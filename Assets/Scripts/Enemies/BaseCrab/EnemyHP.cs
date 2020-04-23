using UnityEngine;
using System.Collections;

public class EnemyHP : MonoBehaviour
{
    public int maxHealth = 3;
    public int currentHealth;
    private Animator anim;
    public bool crabcd;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        anim = gameObject.GetComponent<Animator>();
        crabcd = true;
    }

    private void Update()
    {
        if (currentHealth <= 0)
        {
            anim.SetBool("isDead", true);
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage; // le montant des dommages va être soustrait à la vie actuelle de l'ennemi
    }

    public void DeathAnim()
    {
        GetComponent<Collider2D>().enabled = false; // en cas de mort, le collider du monstre est désactivé
        this.enabled = false;
        GetComponent<EnemyMovement>().enabled = false;
        GetComponent<EnemyAttack>().enabled = false;
        if (GameObject.Find("Boss") != null)
        {
            if (crabcd == true)
            {
                StartCoroutine(Crabcountercd());
                GameObject.Find("Boss").GetComponent<Invoke>().crabcounter--; //sert à compter le nombre de crabes invoqués par le boss et à reset le time de spawn, si j'ai du utilisé Gameobject.Find [...]
                GameObject.Find("Boss").GetComponent<Invoke>().startTimer = 0f;// [...] c'est pcq je ne savais pas comment lier les variables déclarées en public avec un objet qui n'est pas dans la scène. je me tate à mettre ces deux lignes dans un if.
            }

        }
        Destroy(gameObject);
    }

    IEnumerator Crabcountercd()
    {
        crabcd = false;
        yield return new WaitForSeconds(0.2f);
        crabcd = true;
    }
    /*public void Die()
    {
        //afficher l'animation et le sprite de mort
        Debug.Log("Bruh");
        GameObject.Find("Boss").GetComponent<Invoke>().crabcounter--; //sert à compter le nombre de crabes invoqués par le boss et à reset le time de spawn, si j'ai du utilisé Gameobject.Find [...]
        GameObject.Find("Boss").GetComponent<Invoke>().startTimer = 0f;// [...] c'est pcq je ne savais pas comment lier les variables déclarées en public avec un objet qui n'est pas dans la scène. je me tate à mettre ces deux lignes dans un if.

        Destroy(gameObject);
    }*/
}

using UnityEngine;

public class EnemyHP : MonoBehaviour
{
    public int maxHealth = 3;
    public int currentHealth;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage; // le montant des dommages va être soustrait à la vie actuelle de l'ennemi
        if (currentHealth <= 0)
        {
            Die();
        }
    }
    public void Die()
    {
        //afficher l'animation et le sprite de mort
        GameObject.Find("Boss").GetComponent<Invoke>().crabcounter--; //sert à compter le nombre de crabes invoqués par le boss et à reset le time de spawn, si j'ai du utilisé Gameobject.Find [...]
        GameObject.Find("Boss").GetComponent<Invoke>().startTimer = 0f;// [...] c'est pcq je ne savais pas comment lier les variables déclarées en public avec un objet qui n'est pas dans la scène. je me tate à mettre ces deux lignes dans un if.
        Destroy(gameObject);
    }
}

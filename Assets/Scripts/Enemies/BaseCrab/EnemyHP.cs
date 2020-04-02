using UnityEngine;

public class EnemyHP : MonoBehaviour
{
    public bool isDead = false;
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
        GetComponent<Collider2D>().enabled = false; // en cas de mort, le collider du monstre est désactivé
        GetComponent<EnemyMovement>().enabled = false;
        GetComponent<EnemyAttack>().enabled = false;
        GameObject.Find("Boss").GetComponent<Invoke>().crabcounter--;
        Destroy(gameObject);
    }
}

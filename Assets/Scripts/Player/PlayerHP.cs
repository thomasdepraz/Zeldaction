using UnityEngine;

public class PlayerHP : MonoBehaviour
{
    [Header("Player Health")]
    [Range(1, 20)]
    public int maxHealth = 10;
    public int currentHealth;
    public int heal = 2;

    public HealthBar healthBar;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth); // set la barre de vie avec la vie maximale du joueur
    }

    void Update()
    {
        GainLife(heal);
    }
    public void TakeDamage(int damage)
    {
        currentHealth -= damage; // le montant des dommages va être soustrait à la vie actuelle du joueur
        healthBar.SetHealth(currentHealth); // met à jour la barre de vie avec la vie actuelle du joueur

        if (currentHealth <= 0)
        {
            Die();
        }
    }
    void Die()
    {
        GetComponent<Collider2D>().enabled = false; // en cas de mort, le collider du joueur est désactivé
        GetComponent<PlayerMovement>().enabled = false;
        GetComponent<PlayerAttack>().enabled = false;
        this.enabled = false;
    }
    void GainLife(int heal)
    {
        if (Input.GetButtonDown("DebugHeal"))
        {
            currentHealth += heal;
            Debug.Log("pharmacy");
            healthBar.SetHealth(currentHealth);
        }
    }
}

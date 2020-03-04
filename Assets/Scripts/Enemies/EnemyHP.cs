using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHP : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    private Rigidbody2D body;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        body = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage; // le montant des dommages va être soustrait à la vie actuelle de l'ennemi
        if (currentHealth <= 0)
        {
            Die();
        }
    }
    void Die()
    {
        GetComponent<Collider2D>().enabled = false; // en cas de mort, le collider du monstre est désactivé
        this.enabled = false;
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        //body.bodyType = RigidbodyType2D.Kinematic; //lors de l'entrée dans le collider (collision), le RB de l'ennemi devient kinematic pour qu'il ne subisse plus les poussées du joueur.
        Debug.Log("Hello There");
        body.constraints = RigidbodyConstraints2D.FreezeAll; // lui freeze sa position
    }
    void OnCollisionExit2D(Collision2D collision)
    {
        body.constraints = RigidbodyConstraints2D.None; //permet à l'ennemi de ne plus être freeze lorsque le joueur sort de son collider
        //body.bodyType = RigidbodyType2D.Dynamic;
    }
}

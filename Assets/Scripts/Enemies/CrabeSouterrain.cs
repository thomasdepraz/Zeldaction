using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrabeSouterrain : MonoBehaviour
{
    // Variables
    private Rigidbody2D rb;
    private Vector2 direction;

    private GameObject player;
    public PlayerHP playerHP;
    private float distanceToPlayer;

    
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

    [Header("LoadTime")]
    public float loadAttack;
    public float loadMovement;
    public float stunTime = 5f;

    bool canGiveDamage = false;
    bool canMove = true;
    Coroutine LoadAttaque;

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        LoadAttaque = StartCoroutine(LoadAttack());
    }

    void Update()
    {
        Movement();
        CrabeAttack();
    }

    // Déplacement du crabe : Si la distance entre le PJ et le crabe est inférieur à la distance de détection, le crabe se met en mouvement.     
    private void Movement()
    {
        distanceToPlayer = (player.transform.position - gameObject.transform.position).magnitude;

        if(distanceToPlayer <= detectionDistance && canMove)//condition de déplacement
        {
            direction = player.transform.position - gameObject.transform.position;
            rb.velocity = direction.normalized * speed * Time.deltaTime;
        }
        else
        {
            rb.velocity = Vector2.zero;            
        }
    }

    private void CrabeAttack() // Fonction de déclenchement de l'attaque 
    {
        if(distanceToPlayer <= attackDistance)//Condition au déclenchement
        {
            rb.velocity = Vector2.zero;
            canMove = false;
            canGiveDamage = true;
            StartCoroutine(LoadAttack());
            StartCoroutine(LoadMovement());              
        }
        else //Annulation du déclenchement
        {            
            canGiveDamage = false;
            StopCoroutine(LoadAttaque);
        }

    }
    IEnumerator LoadAttack() 
    {
        yield return new WaitForSeconds(loadAttack);
        if (canGiveDamage == true)
        {            
            playerHP.TakeDamage(attackDamage);            
        }
    }

    IEnumerator LoadMovement()
    {
        yield return new WaitForSeconds(loadMovement);
        canMove = true;
    }

    // Il manque l'élimination du crabe qui s'effectue avec le pull du crabe via l'hameçon du PJ.
}

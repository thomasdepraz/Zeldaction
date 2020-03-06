using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrabeSouterrain : MonoBehaviour
{
    // Variables
    private Rigidbody2D rb;
    private Vector2 direction;

    private GameObject player;
    private float distanceToPlayer;

    
    [Header("Tweaks")]
    [Range(0f, 5f)]
    public float speed;
    [Range(0f, 5f)]
    public float detectionDistance;
    [Range(0f, 5f)]
    public float attackDistance;

    public GameObject pinceCrabe;

    bool canTakeDamage = false;
    public float loadAttack;
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

        if(distanceToPlayer <= detectionDistance)//condition de déplacement
        {
            direction = player.transform.position - gameObject.transform.position;
            rb.velocity = direction.normalized * speed * Time.deltaTime;
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
    }

    private void CrabeAttack()
    {
        if(distanceToPlayer <= attackDistance)//Condition au déclenchement
        {
            rb.velocity = Vector2.zero;
            canTakeDamage = true;
            StartCoroutine(LoadAttack());
        }
        else
        {
            canTakeDamage = false;
            StopCoroutine(LoadAttaque);
        }

    }
    IEnumerator LoadAttack()
    {
        yield return new WaitForSeconds(loadAttack);
        if(canTakeDamage == true)
        {
            Debug.Log("le PJ prend des dégâts");
        }
    }
}

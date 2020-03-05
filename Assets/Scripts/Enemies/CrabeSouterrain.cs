using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrabeSouterrain : MonoBehaviour
{
    // Variables
    private Rigidbody2D rb;
    private Vector2 direction;

    private CircleCollider2D attackZone;
    private GameObject player;

    [Header("Tweaks")]
    [Range(0f, 5f)]
    public float speed;
    [Range(0f, 5f)]
    public float detectionDistance;

    private float distanceToPlayer;

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        attackZone = gameObject.GetComponent<CircleCollider2D>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        
    }

    // Déplacement du crabe : Si la distance entre le PJ et le crabe est inférieur à la distance de détection, le crabe se met en mouvement.     
    private void Movement()
    {
        distanceToPlayer = (player.transform.position - gameObject.transform.position).magnitude;

        if(distanceToPlayer <= detectionDistance)//condition d déplacement
        {
            direction = player.transform.position - gameObject.transform.position;
            rb.velocity = direction.normalized * speed * Time.deltaTime;
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
    }
    
}

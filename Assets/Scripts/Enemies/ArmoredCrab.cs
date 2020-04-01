using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmoredCrab : MonoBehaviour
{
    [Header("Target")]
    private GameObject player;
    private Rigidbody2D enemyRb;

    [HideInInspector] public Vector2 direction;
    [HideInInspector] public bool canMove = true;

    [Header("Movement & Behavior")]
    [Range(0f, 300f)]
    public float enemySpeed = 150f;
    public float patrolRadius;
    public float minStopDistance;

    private Vector2 randomPatrolPosition;
    private float latestDirectionChangeTime;
    private readonly float directionChangeTime = 3f;
    private Vector2 patrolCenterPosition;
    private Vector2 targetPosition;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        enemyRb = GetComponent<Rigidbody2D>();

        latestDirectionChangeTime = 0f;
        patrolCenterPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //Si le joueuer n'est pas détécté, l'ennemi patrouille
        Patrol();
        if (canMove && Time.time - latestDirectionChangeTime > directionChangeTime)
        {
            latestDirectionChangeTime = Time.time;
            NewPatrolDirection();
        }
        //Si le joueur est détecté, alors il lui jette ses projectile

        //Si le joueur est suffisament près alors le crabe l'attaque au corps a corps

    }

    void NewPatrolDirection()
    {
        randomPatrolPosition = patrolCenterPosition + new Vector2(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f)) * patrolRadius;
    }

    void Patrol()
    {
        targetPosition = randomPatrolPosition;
        
        if (canMove)
        {
            if (Vector2.Distance(transform.position, targetPosition) > minStopDistance)
                enemyRb.velocity = (targetPosition - (Vector2)transform.position).normalized * enemySpeed * Time.fixedDeltaTime;
            
            else
                enemyRb.velocity = Vector2.zero;
        }
    }
}

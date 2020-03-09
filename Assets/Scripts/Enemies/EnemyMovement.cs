using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public Transform player;
    private Rigidbody2D enemyRb;
    [HideInInspector] public Vector2 direction;
    public float enemySpeed = 150f;
    public bool canMove = true;
    public float patrolRadius;
    private Vector2 randomPatrolPosition;
    private float latestDirectionChangeTime;
    private readonly float directionChangeTime = 3f;
    private Vector2 patrolCenterPosition;
    private Vector2 targetPosition;
    public float minStopDistance;

    // Start is called before the first frame update
    void Start()
    {
        enemyRb = GetComponent<Rigidbody2D>();
        latestDirectionChangeTime = 0f;
        patrolCenterPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        MoveTowardsTarget();
        if (canMove && Time.time - latestDirectionChangeTime > directionChangeTime)
        {
            latestDirectionChangeTime = Time.time;
            WanderAround();
        }
    }

    /* void followPlayer()
     {
         direction = (player.transform.position - transform.position);
         enemyRb.velocity = direction.normalized * enemySpeed * Time.deltaTime;
     }*/
    void WanderAround()
    {
        randomPatrolPosition = patrolCenterPosition + new Vector2(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f)) * patrolRadius;
    }

    void MoveTowardsTarget()
    {
        targetPosition = randomPatrolPosition;
        if (canMove)
        {
            if (Vector2.Distance(transform.position, targetPosition) > minStopDistance)
            {
                enemyRb.velocity = (targetPosition - (Vector2)transform.position).normalized * enemySpeed * Time.fixedDeltaTime;
            }
            else
            {
                enemyRb.velocity = Vector2.zero;
            }
        }

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        enemyRb.constraints = RigidbodyConstraints2D.FreezeAll; // lui freeze sa position
    }
    void OnCollisionExit2D(Collision2D collision)
    {
        enemyRb.constraints = RigidbodyConstraints2D.None; //permet à l'ennemi de ne plus être freeze lorsque le joueur sort de son collider
    }
}

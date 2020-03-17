using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [Header ("Target")]
    public Transform player;
    private Rigidbody2D enemyRb;

    [HideInInspector] public Vector2 direction;
    [HideInInspector] public bool canMove = true;

    [Header ("Movement & Behavior")]
    [Range (0f, 300f)]
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
        enemyRb = GetComponent<Rigidbody2D>();
        latestDirectionChangeTime = 0f;
        patrolCenterPosition = transform.position;
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
        //enemyRb.constraints = RigidbodyConstraints2D.FreezeRotation;
    }
}

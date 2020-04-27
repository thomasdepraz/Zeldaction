using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private Transform player;
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

    private Animator anim;
    private float horizontalOrientation;
    private float verticalOrientation;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        enemyRb = GetComponent<Rigidbody2D>();
        latestDirectionChangeTime = 0f;
        patrolCenterPosition = transform.position;
        anim = gameObject.GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        MoveTowardsTarget();
        if (canMove && Time.time - latestDirectionChangeTime > directionChangeTime)
        {
            latestDirectionChangeTime = Time.time;
            WanderAround();
            anim.SetBool("isMoving", true);
        }
        Orientation();

        if (enemyRb.velocity == Vector2.zero)
        {
            anim.SetBool("isMoving", false);
        }
        else
        {
            anim.SetBool("isMoving", true);
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
                anim.SetFloat("HorizontalMovement", enemyRb.velocity.x);
                anim.SetFloat("VerticalMovement", enemyRb.velocity.y);
            }
            else
            {
                enemyRb.velocity = Vector2.zero;
            }
        }
    }

    void Orientation()
    {
        Vector2 orientation = player.position - gameObject.transform.position;

        horizontalOrientation = orientation.normalized.x;
        verticalOrientation = orientation.normalized.y;

        anim.SetFloat("HorizontalOrientation", horizontalOrientation);
        anim.SetFloat("VerticalOrientation", verticalOrientation);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            enemyRb.constraints = RigidbodyConstraints2D.FreezeAll; // lui freeze sa position
        if (collision.gameObject.layer == 8)
        {
            enemyRb.velocity = Vector2.zero;
        }
    }
    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            enemyRb.constraints &= ~RigidbodyConstraints2D.FreezePositionX; //permet à l'ennemi de ne plus être freeze lorsque le joueur sort de son collider
            enemyRb.constraints &= ~RigidbodyConstraints2D.FreezePositionY;
        }
    }
}

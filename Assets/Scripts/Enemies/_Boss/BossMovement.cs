using UnityEngine;

public class BossMovement : MonoBehaviour
{
    private Rigidbody2D bossRb;
    [HideInInspector] public bool canMove = true;
    [Header("Movement & Behavior")]
    [Range(0f, 300f)]
    public float bossSpeed = 150f;
    public float patrolRadius;
    public float minStopDistance;

    private Vector2 patrolPosition;
    private float latestDirectionChangeTime;
    private readonly float directionChangeTime = 3f;
    private Vector2 patrolCenterPosition;
    public Vector2 targetPosition;

    void Start()
    {
        bossRb = GetComponent<Rigidbody2D>();
        latestDirectionChangeTime = 0f;
        patrolCenterPosition = transform.position;
    }
    void FixedUpdate()
    {
        MoveTowardsTarget();
        if (canMove && Time.time - latestDirectionChangeTime > directionChangeTime)
        {
            latestDirectionChangeTime = Time.time;
            Patrol();
        }
    }
    void Patrol()
    {
        patrolPosition = patrolCenterPosition + new Vector2(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f)) * patrolRadius;
    }
    void MoveTowardsTarget()
    {
        if (canMove)
        {
            if (Vector2.Distance(transform.position, targetPosition) > minStopDistance)
            {
                bossRb.velocity = (patrolPosition - (Vector2)transform.position).normalized * bossSpeed * Time.fixedDeltaTime;
            }
            else
            {
                bossRb.velocity = Vector2.zero;
            }
        }

    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        bossRb.constraints = RigidbodyConstraints2D.FreezeAll;
    }
    void OnCollisionExit2D(Collision2D collision)
    {
        bossRb.constraints = RigidbodyConstraints2D.None;
    }
}

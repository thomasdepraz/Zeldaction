using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BossMovement : MonoBehaviour
{
    private Rigidbody2D bossRb;
    [HideInInspector] public bool canMove;
    [Header("Movement & Behavior")]
    [Range(0f, 300f)]
    public float bossSpeed = 150f;
    public float patrolRadius;
    public float minStopDistance;

    private Vector2 patrolCenterPosition;
    public Vector2 targetPosition;
    Transform player;

    void Start()
    {
        bossRb = GetComponent<Rigidbody2D>();
        patrolCenterPosition = transform.position;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        canMove = true;
    }
    void FixedUpdate()
    {
        if (Vector2.Distance(player.position, bossRb.position) >= 2.5f && canMove == true)
        {
            Patrol();
            StartCoroutine(MoveTimer());
        }
        else if (Vector2.Distance(player.position, bossRb.position) <= 3.5f)
        {
            bossRb.velocity = Vector2.zero;
        }
    }
    void Patrol()
    {
        Vector2 target = new Vector2(Random.Range(-5.0f, 5.0f), patrolCenterPosition.y);
        Vector2 direction = target - (Vector2)transform.position;
        bossRb.velocity = direction.normalized * bossSpeed * Time.fixedDeltaTime;
    }

    IEnumerator MoveTimer()
    {
        canMove = false;
        yield return new WaitForSeconds(2f);
        canMove = true;
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        bossRb.constraints = RigidbodyConstraints2D.FreezeAll;
    }
    void OnCollisionExit2D(Collision2D collision)
    {
        bossRb.constraints &= ~RigidbodyConstraints2D.FreezePositionX;
        bossRb.constraints &= ~RigidbodyConstraints2D.FreezePositionY;
    }
}

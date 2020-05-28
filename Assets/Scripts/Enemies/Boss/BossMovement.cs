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
    public Animator anim;
    private Vector2 direction;
    private Vector2 patrolCenterPosition;
    private Vector2 target;
    Transform player;
    private bool changedirection = true;
    private float attackRange = 2.5f;

    void Start()
    {
        bossRb = GetComponent<Rigidbody2D>();
        patrolCenterPosition = transform.position;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        canMove = false;
    }
    void FixedUpdate()
    {


        if (Vector2.Distance(player.position, bossRb.position) >= attackRange && canMove == true)
        {
            if (changedirection == true)
            {
                StartCoroutine(ChangeDirection());
            }
            bossRb.velocity = direction.normalized * bossSpeed * Time.fixedDeltaTime;
            if (bossRb.velocity != Vector2.zero)
            {
                anim.SetBool("isRunning", true);
            }
        }

        else if (Vector2.Distance(player.position, bossRb.position) < attackRange || canMove == false)
        {
            bossRb.velocity = Vector2.zero;
            anim.SetBool("isRunning", false);
        }
    }

    IEnumerator ChangeDirection()
    {
        changedirection = false;
        target = new Vector2(Random.Range(-5.0f, 5.0f), patrolCenterPosition.y);
        direction = target - (Vector2)transform.position;
        yield return new WaitForSeconds(1f);
        changedirection = true;
    }

   /* void OnCollisionEnter2D(Collision2D collision)
    {
        bossRb.constraints = RigidbodyConstraints2D.FreezeAll;
    }*/
    void OnCollisionExit2D(Collision2D collision)
    {
        bossRb.constraints &= ~RigidbodyConstraints2D.FreezePositionX;
        bossRb.constraints &= ~RigidbodyConstraints2D.FreezePositionY;
    }
}

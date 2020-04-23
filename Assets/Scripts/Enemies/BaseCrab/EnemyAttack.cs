using System.Collections;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [Header("Variables")]
    private bool playerIsDetected;
    [Range(0, 5)]
    public float detectionDistance;
    [Range(0, 5)]
    public int attackDamage = 2;
    private bool coroutineCanStart = true;
    [HideInInspector] public bool enemysIsTackling;
    private Rigidbody2D enemyRb;

    [Header("Charge Details")]
    public float chargeStopDistance;
    public float enemyChargeSpeed;
    [Range(0f, 1f)]
    public float chargeMaxTime;
    [Range(0f, 0.5f)]
    public float chargeRadiusTriggerAttack;
    [Range(0f, 1f)]
    public float prepairTime;
    [Range(0f, 5f)]
    public float stunTime;

    private EnemyMovement enemyMovement;

    [Header("Target")]
    public LayerMask playerLayer;
    private GameObject player;
    private PlayerHP playerHP;

    private Animator anim;


    // Start is called before the first frame update
    void Start()
    {
        enemyMovement = GetComponent<EnemyMovement>();
        enemyRb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        playerHP = player.GetComponent<PlayerHP>();
        anim = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerIsDetected == true && coroutineCanStart == true)
        {
            StartCoroutine(Charge());
        }

        if ((player.transform.position - transform.position).magnitude < detectionDistance)
        {
            playerIsDetected = true;
        }
        else
        {
            playerIsDetected = false;
        }
    }

    IEnumerator Charge()
    {
        coroutineCanStart = false;

        enemyMovement.canMove = false;
        enemyRb.velocity = Vector2.zero;
        //play animation (courir sur place)
        yield return new WaitForSeconds(prepairTime);
        anim.SetBool("isAttacking", true);
        float chargeTime = 0f;
        Vector2 direction = player.transform.position - transform.position;
        direction.Normalize();
        do
        {
            chargeTime += Time.fixedDeltaTime;
            enemyRb.velocity = direction.normalized * enemyChargeSpeed * Time.deltaTime;
            yield return new WaitForFixedUpdate();
            if (Physics2D.OverlapCircle(transform.position, chargeRadiusTriggerAttack, playerLayer))
            {
                Debug.Log("Hello there");
                anim.SetBool("isAttacking", false);
                playerHP.TakeDamage(attackDamage);
            }
        }
        while (chargeTime < chargeMaxTime && !Physics2D.OverlapCircle(transform.position, chargeRadiusTriggerAttack, playerLayer));

        enemyRb.velocity = Vector2.zero;
        anim.SetBool("isAttacking", false);
        yield return new WaitForSeconds(stunTime);
        coroutineCanStart = true;
        enemyMovement.canMove = true;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, detectionDistance);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, chargeRadiusTriggerAttack);
    }
}
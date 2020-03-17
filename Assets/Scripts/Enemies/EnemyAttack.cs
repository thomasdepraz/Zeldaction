using System.Collections;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [Header("Variables")]
    private bool playerIsDetected;
    [Range (0,5)]
    public int attackDamage = 2;
    private bool coroutineCanStart = true;
    [HideInInspector]public bool enemysIsTackling;
    private Rigidbody2D enemyRb;

    [Header ("Charge Details")]
    public float chargeStopDistance;
    public float enemyChargeSpeed;
    [Range (0f,1f)]
    public float chargeMaxTime;
    [Range(0f,0.5f)]
    public float chargeRadiusTriggerAttack;
    [Range(0f,1f)]
    public float prepairTime;
    [Range(0f,5f)]
    public float stunTime;

    private EnemyMovement enemyMovement;

    [Header ("Target")]
    public LayerMask playerLayer;
    public PlayerHP playerHP;


    // Start is called before the first frame update
    void Start()
    {
        enemyMovement = GetComponent<EnemyMovement>();
        enemyRb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerIsDetected == true && coroutineCanStart == true)
        {
            StartCoroutine(Charge());
        }
    }
    void OnTriggerEnter2D(Collider2D collider)
    {
        playerIsDetected = true;
    }
    void OnTriggerExit2D(Collider2D collider)
    {
        playerIsDetected = false;
    }
    IEnumerator Charge()
    {
        coroutineCanStart = false;
        enemyMovement.canMove = false;
        enemyRb.velocity = Vector2.zero;
        //play animation (courir sur place)
        yield return new WaitForSeconds(prepairTime);
        float chargeTime = 0f;
        Vector2 direction = playerHP.transform.position - transform.position;
        direction.Normalize();
        do
        {
            chargeTime += Time.fixedDeltaTime;
            enemyRb.velocity = direction.normalized * enemyChargeSpeed * Time.deltaTime;
            yield return new WaitForFixedUpdate();
            if (Physics2D.OverlapCircle(transform.position, chargeRadiusTriggerAttack, playerLayer))
            {
                playerHP.TakeDamage(attackDamage);
            }
        }
        while (chargeTime < chargeMaxTime && !Physics2D.OverlapCircle(transform.position, chargeRadiusTriggerAttack, playerLayer));

        enemyRb.velocity = Vector2.zero;
        yield return new WaitForSeconds(stunTime);
        coroutineCanStart = true;
        enemyMovement.canMove = true;
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(gameObject.transform.position, 2.5f);
        Gizmos.color = Color.red;
    }
}
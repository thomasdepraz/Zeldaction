using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    private bool playerIsDetected;
    public int attackDamage = 40;
    private bool coroutineCanStart = true;
    public bool enemysIsTackling;
    public float chargeStopDistance;
    private Rigidbody2D enemyRb;
    public float enemyChargeSpeed;
    public LayerMask playerLayer;
    public float chargeMaxTime;
    public float chargeRadiusTriggerAttack;
    public float prepairTime;
    public float stunTime;

    private EnemyMovement enemyMovement;
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
        do
        {
            chargeTime += Time.fixedDeltaTime;
            enemyRb.velocity = enemyMovement.direction.normalized * enemyChargeSpeed * Time.deltaTime;
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
}
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
    private float distanceToPlayer;
    public float detectionDistance;

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

    //Orientation 
    private Vector2 orientation;
    private float verticalOrientation;
    private float horizontalOrientation;

    [Header("Shoot")]
    public GameObject projectile;
    [Range(0f,5f)]
    public float projectileSpeed = 3f;
    private bool canShoot = true;

    [Header("MeleeAttack")]
    public Transform attackPoint;
    public LayerMask hitboxLayer;
    public float meleeAttackRadius;
    public int meleeAttackDamage;
    private bool canAttack = true;



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
        Orientation();
        distanceToPlayer = (gameObject.transform.position - player.transform.position).magnitude;
        if (distanceToPlayer < detectionDistance)
        {
            Shoot();
        }
        else
        {
            //Si le joueuer n'est pas détécté, l'ennemi patrouille
            Patrol();
            if (canMove && Time.time - latestDirectionChangeTime > directionChangeTime)
            {
                latestDirectionChangeTime = Time.time;
                NewPatrolDirection();
            }
        }

        Attack();
    }

    void Orientation()
    {
        orientation = player.transform.position - transform.position;
        horizontalOrientation = orientation.normalized.x;
        verticalOrientation = orientation.normalized.y;

        if (Mathf.Abs(horizontalOrientation) > Mathf.Abs(verticalOrientation))
        {
            if(horizontalOrientation > 0)
                attackPoint.eulerAngles = new Vector3(0.0f, 0.0f, 0);

            else
                attackPoint.localEulerAngles = new Vector3(0.0f, 0.0f, 180);     
        }
        else
        {
            if (verticalOrientation > 0)
                attackPoint.localEulerAngles = new Vector3(0.0f, 0.0f, 90);
          
            else
                attackPoint.localEulerAngles = new Vector3(0.0f, 0.0f, -90);
        }                     
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

    void MoveToPlayer()
    {
        if (Vector2.Distance(transform.position, player.transform.position) > minStopDistance)
            enemyRb.velocity = (player.transform.position - transform.position).normalized * enemySpeed * Time.fixedDeltaTime;

        else
            enemyRb.velocity = Vector2.zero;
    }

    void Shoot()//Lancer cette fonction depuis l'animator
    {
        if (canShoot)
        {
            StartCoroutine("ShootProjectiles");
            canMove = false;
        }
    }

    void EnemyCanMove()
    {
        canMove = true;
    }

    void Attack()
    {
        
        
        Collider2D[] hit = Physics2D.OverlapCircleAll(attackPoint.transform.position, meleeAttackRadius, hitboxLayer);
        foreach(Collider2D col in hit)
        {
            if(col.CompareTag("Player") && canAttack)
            {
                player.GetComponent<PlayerHP>().TakeDamage(meleeAttackDamage);
                StartCoroutine("CanAttack");
                canAttack = false;
                canShoot = false;
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, detectionDistance);
        Gizmos.color = Color.red;

        Gizmos.DrawWireSphere(attackPoint.transform.position, meleeAttackRadius);
        Gizmos.color = Color.blue;
    }

    private IEnumerator ShootProjectiles()
    {
        canShoot = false;
          
        GameObject Projectile = GameObject.Instantiate(projectile, transform.position, Quaternion.identity);
        Projectile.transform.position = player.transform.position;
        
        yield return new WaitForSeconds(1f);//Wait for projectile traveltime

        Projectile.GetComponent<ArmoredCrabProjectile>().Explode();//activate Projectile damage

        canShoot = true;
    }

    private IEnumerator CanAttack()
    {
        yield return new WaitForSeconds(3);
        canAttack = true;
        canShoot = true;
    }
    public void GetAnimationEvent(string parameter)
    {
        if(parameter == "parameter2")
        {
            //Do something
        }

        if(parameter == "parameter3")
        {
            //Do something else
        }

        //...
    }


}

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
    public GameObject armor;
    public GameObject baseCrab;

    //Orientation 
    private Vector2 orientation;
    private float verticalOrientation;
    private float horizontalOrientation;

    [Header("Shoot")]
    public GameObject projectile;
    [Range(0f,5f)]
    public float projectileSpeed = 3f;
    private bool canShoot = false;
    private bool canDetect = true;

    [HideInInspector]public Animator anim;



    // Start is called before the first frame update

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        enemyRb = GetComponent<Rigidbody2D>();

        latestDirectionChangeTime = 0f;
        patrolCenterPosition = transform.position;

        anim = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Orientation();
        distanceToPlayer = (gameObject.transform.position - player.transform.position).magnitude;
        if (distanceToPlayer < detectionDistance)
        {
            if(canDetect)
            { 
                anim.SetBool("Detected", true);
            }
            Shoot();
        }
        else
        {
            anim.SetBool("Detected", false);
            //Si le joueuer n'est pas détécté, l'ennemi patrouille
            Patrol();
            if (canMove && Time.time - latestDirectionChangeTime > directionChangeTime)
            {
                latestDirectionChangeTime = Time.time;
                NewPatrolDirection();
            }
        }

        if(!canMove)
        {
            enemyRb.velocity = Vector2.zero;
        }
        if(enemyRb.velocity == Vector2.zero)
        {
            anim.SetBool("isMoving", false);
        }
        else
        {
            anim.SetBool("isMoving", true);
        }
        //Attack();
    }

    void Orientation()
    {
        orientation = player.transform.position - transform.position;
        horizontalOrientation = orientation.normalized.x;
        verticalOrientation = orientation.normalized.y;
        anim.SetFloat("HorizontalOrientation", horizontalOrientation);
        anim.SetFloat("VerticalOrientation", verticalOrientation);               
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
            {
                enemyRb.velocity = (targetPosition - (Vector2)transform.position).normalized * enemySpeed * Time.fixedDeltaTime;
                anim.SetFloat("HorizontalMovement", enemyRb.velocity.x);
                anim.SetFloat("VerticalMovement", enemyRb.velocity.y);
            }


            else
                enemyRb.velocity = Vector2.zero;
        }
        else
        {
            enemyRb.velocity = Vector2.zero;
        }
    }


    void Shoot()//Lancer cette fonction depuis l'animator
    {
        if (canShoot)
        {
            StartCoroutine("ShootProjectiles");
        }
    }

    private IEnumerator ShootProjectiles()
    {
        canShoot = false;
        
        GameObject Projectile = Instantiate(projectile, transform.position, Quaternion.identity);
        Projectile.transform.position = player.transform.position;
        yield return new WaitForSeconds(1f);//Wait for projectile traveltime

        Projectile.GetComponent<ArmoredCrabProjectile>().Explode();//activate Projectile damage
    }

    private IEnumerator Stun()
    {
        yield return new WaitForSeconds(2);
        canDetect = true;
        canMove = true;
    }
  
    public void GetAnimationEvent(string parameter)
    {
        if(parameter == "shoot")
        {
            canShoot = true;
        }

        if(parameter == "startedShooting")
        {
            canMove = false;
        }

        if(parameter == "finishedShooting")
        {
            anim.SetBool("isShooting", false);
            StartCoroutine("Stun");
        }

        if(parameter == "detected")
        {
            anim.SetBool("isShooting", true);
            anim.SetBool("Detected", false);
            canDetect = false;
            Debug.Log("Detected");
        }

        if(parameter == "deathEnded")
        {
            GameObject Crab = Instantiate(baseCrab, gameObject.transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }


}

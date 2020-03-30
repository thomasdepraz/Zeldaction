using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttack : MonoBehaviour
{
    [Header("Variables")]
    private bool playerIsDetected;
    [Range(0, 5)]
    public int attackDamage = 2;
    private bool coroutineCanStart = true;
    [HideInInspector] public bool enemysIsClawing;


    [Header("Charge Details")]
    public float chargeStopDistance;
    public float enemyChargeSpeed;
    [Range(0f, 1f)]
    public float clawMaxTime;
    [Range(0f, 0.5f)]
    public float clawRadiusTriggerAttack;
    [Range(0f, 1f)]
    public float prepairTime;
    [Range(0f, 5f)]
    public float stunTime;



    [Header("Target")]
    public LayerMask playerLayer;
    public PlayerHP playerHP;

    float rotationRadius = 2f, angularSpeed = 2f;

    public float posX, posY = 0f;
    public float angle = 0f;
    public float newPos;

    float detectAngle;
    float attackRange;

    public Transform player;

    [Header("Claw")]
    public GameObject claw;

    public GameObject milieu;


    // Update is called once per frame
    void Update()
    {
        if (playerIsDetected == true /*&& coroutineCanStart == true*/)
        {
            StartCoroutine(Claw());
        }
    }
    private void FixedUpdate()
    {

    }
    void OnTriggerEnter2D(Collider2D collider)
    {
        playerIsDetected = true;
    }
    void OnTriggerExit2D(Collider2D collider)
    {
        playerIsDetected = false;
    }

    IEnumerator Claw()
    {
        coroutineCanStart = false;
        float destination = Mathf.PI;
        if (angle > -destination)
        {
            posX = transform.position.x + Mathf.Cos(angle) * rotationRadius;
            posY = transform.position.y + Mathf.Sin(angle) * rotationRadius;
            claw.transform.position = new Vector2(posX, posY);
            //claw.transform.position = Vector2.Lerp(claw.transform.position, milieu.transform.position );
            angle = angle - Time.deltaTime * angularSpeed;
            if (Mathf.Abs(angle) >= 2 * Mathf.PI)
                angle = 0f;
            yield return new WaitForSeconds(prepairTime);

            Collider2D[] hitPlayer = Physics2D.OverlapCircleAll(claw.transform.position, attackRange);


            //claw.transform.localEulerAngles = new Vector3(0.0f, 0.0f, detectAngle);
            //float chargeTime = 0f;
            yield return new WaitForSeconds(stunTime);
            coroutineCanStart = true;
        }
    }

}
/* Quand je rentre dans la zone de collision du boss, il se dirige vers moi puis, quand je rentre dans sa zone d'aggression, donne un coup de pince à la position que j'ai au moment ou il
 décide d'attaquer.
 Son coup de pince prends la forme d'un collider sur un gameobject enfant "pince", qui va effectuer un quart de rotation autour du boss.
 1ere solution envisagée: circle collider, quand j'entre dedans, le bosse s'arrete et donne un coup qui va tourner autour de ce collider*/

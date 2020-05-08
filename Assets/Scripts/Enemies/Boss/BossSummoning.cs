using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSummoning : MonoBehaviour
{
    public static BossSummoning Instance;
    [Header("Distance minimale avant laquelle le boss invoque")]
    [Range(0, 5)]
    public float attackRange = 2.5f;
    private Rigidbody2D rb;
    public Transform player;
    [HideInInspector]
    public float startTimer;
    public float timer;
    [HideInInspector]
    public GameObject baseCrab;
    public GameObject armoredCrab;
    [HideInInspector]
    public Animator anim;
    [Header("Compteur de crabes invoqués actuellement par le boss")]
    public int crabcounter = 0;
    public GameObject Waypoint0;
    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        startTimer = 0f;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        timer = startTimer += Time.deltaTime;
        if (Vector2.Distance(player.position, rb.position) >= attackRange && crabcounter < 2)
        {
            if (timer >= 5f && GetComponent<BossManager>().isPhase1 == true) // && phase 1
            {
                anim.SetTrigger("SummonPhase1");
                StartCoroutine(StopMoving());
                startTimer = -3f;
            }
            else if (timer >= 5f && GetComponent<BossManager>().isPhase2 == true && crabcounter <2) // && phase 2
            {
                anim.SetTrigger("SummonPhase2");
                startTimer = -3f;
            }
        }
        else if (Vector2.Distance(player.position, rb.position) <= attackRange)
        {
            startTimer = 0f;
        }
    }
    void SummonPhase1()// est utilisé par l'animator
    {
        Instantiate(baseCrab, Waypoint0.transform.position, transform.rotation);
        baseCrab.transform.position = new Vector2(transform.position.x, transform.position.y -1f);
        crabcounter++;
    }
    void SummonPhase2()
    {
        armoredCrab.transform.position = new Vector2(transform.position.x, -1f);
        Instantiate(armoredCrab);
        crabcounter++;
    }
    IEnumerator StopMoving()
    {
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        yield return new WaitForSeconds(2f);
        rb.constraints &= ~RigidbodyConstraints2D.FreezePositionX;
        rb.constraints &= ~RigidbodyConstraints2D.FreezePositionY;
    }
}

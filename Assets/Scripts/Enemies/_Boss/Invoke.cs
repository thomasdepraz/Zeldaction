using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invoke : MonoBehaviour
{
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
    private Animator anim;
    [Header("Compteur de crabes invoqués actuellement par le boss")]
    public int crabcounter = 0;
    // Start is called before the first frame update
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
            /*if (timer >= 5f) // && phase 1
            {
                anim.SetTrigger("Summon_Phase1");
                StartCoroutine(StopMoving());
                startTimer = -3f;
            }*/
            if (timer >= 5f) // && phase 2
            {
                anim.SetTrigger("Summon_Phase2");
                startTimer = -3f;
            }
        }
        else if (Vector2.Distance(player.position, rb.position) <= attackRange)
        {
            startTimer = 0f;
        }
    }
    void Summon_Phase1()// est utilisé par l'animator
    {
        Instantiate(baseCrab);
        baseCrab.transform.position = new Vector2(transform.position.x, -1f);
        crabcounter++;
    }
    void Summon_Phase2()
    {
        Instantiate(armoredCrab);
        armoredCrab.transform.position = new Vector2(transform.position.x, -1f);
        crabcounter++;
    }
    IEnumerator StopMoving()
    {
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        yield return new WaitForSeconds(2f);
        rb.constraints = RigidbodyConstraints2D.None;
    }
}

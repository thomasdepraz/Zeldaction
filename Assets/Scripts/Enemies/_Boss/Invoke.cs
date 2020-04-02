using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invoke : MonoBehaviour
{
    public bool canSummon;
    public float attackRange = 2.5f;
    private Rigidbody2D rb;
    public Transform player;
    private float startTimer;
    public float timer;
    public GameObject baseCrab;
    private Animator anim;
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
        if (Vector2.Distance(player.position, rb.position) >= attackRange && crabcounter<2)
        {
            if (timer >= 5f)
            {
                anim.SetTrigger("Summon");
                startTimer = 0f;
            }
        }
    }
    void Summon()
    {
        Instantiate(baseCrab);
        baseCrab.transform.position = new Vector2 (transform.position.x, -1f) ;
        canSummon = false;
        startTimer = 0f;
        crabcounter++;
    }
    void Crabcounter()
    {
        {
            Debug.Log("Gangnam Style");
            crabcounter--;
        }
    }
}

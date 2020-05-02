using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossLegProjectile : MonoBehaviour
{

    [Header("Elements")]
    public Animator anim;

    [Header("Logic")]
    public int projectileDamage;

    [Header("Tweaks")]
    [Range(0, 5)]
    public float strikeRange;


    private GameObject player;
    public float startTimer;
    public float timer;
    private bool legHit;
    public bool Missed;



    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        legHit = false;
        Missed = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Explode()
    {
        //Lancer l'anim d'explo
        if ((player.transform.position - gameObject.transform.position).magnitude <= strikeRange)
        {
            GameObject.Find("Boss").GetComponent<BossLegThrow>().hasHit = true;
            player.GetComponent<PlayerHP>().TakeDamage(projectileDamage);
            legHit = true;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, strikeRange);
        Gizmos.color = Color.red;
    }

    public void GetAnimationEvent(string parameter)
    {
        if (parameter == "explode")
        {
            anim.SetBool("Explode", true);
            Explode();
        }
        if (parameter == "endExplosion")
        {
            if (legHit == true)
            {
                Destroy(gameObject);
            }
            else
            {
                gameObject.GetComponent<BoxCollider2D>().enabled = true;
                startTimer = 0f;
                Missed = true;
            }
        }
    }
}
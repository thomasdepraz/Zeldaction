using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossLegProjectile : MonoBehaviour
{
    public bool hasHit;

    [Header("Elements")]
    public Animator anim;

    [Header("Logic")]
    public int projectileDamage;

    [Header("Tweaks")]
    [Range(0, 5)]
    public float strikeRange;


    private GameObject player;
    public GameObject boss;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
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
            player.GetComponent<PlayerHP>().TakeDamage(projectileDamage);
            boss.GetComponent<BossLegThrow>().hasHit = true;
        }
        
        else
        {
            hasHit = false;
            boss.GetComponent<BossLegThrow>().hasHit = false;
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
            if (boss.GetComponent<BossLegThrow>().hasHit == true)
            {
                Destroy(gameObject);
            }
            else
            {
                gameObject.GetComponent<BoxCollider2D>().enabled = true;
            }
        }
    }
}
//a voir comment rework le getanimation event

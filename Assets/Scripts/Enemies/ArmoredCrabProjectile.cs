using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmoredCrabProjectile : MonoBehaviour
{

    [Header("Elements")]
    public Animator anim;

    [Header("Logic")]
    public int projectileDamage;

    [Header("Tweaks")]
    [Range(0,5)]
    public float explodeRange;


    private GameObject player;


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
        if ((player.transform.position - gameObject.transform.position).magnitude <= explodeRange)
        {
            player.GetComponent<PlayerHP>().TakeDamage(projectileDamage);
        }     
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, explodeRange);
        Gizmos.color = Color.red;
    }

    public void GetAnimationEvent(string parameter)
    {
        if(parameter == "explode")
        {
            anim.SetBool("Explode", true);
            Explode();
        }
        if(parameter =="endExplosion")
        {
            Destroy(gameObject);//POUR l'INSTANT, après détruire depuis l'anim
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmoredCrabProjectile : MonoBehaviour
{

    [Header("Elements")]
    public Rigidbody2D projectileRb;
    public CircleCollider2D col;

    [Header("Logic")]
    public int projectileDamage;
    public LayerMask hitBoxLayer;


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
        Collider2D[] hit = Physics2D.OverlapCircleAll(transform.position, col.radius, hitBoxLayer);
        foreach (Collider2D col in hit)
        {
            if (col.CompareTag("Player"))
            {
                player.GetComponent<PlayerHP>().TakeDamage(projectileDamage);
            }
        }

        Destroy(gameObject);//POUR l'INSTANT, après détruire depuis l'anim
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, col.radius);
        Gizmos.color = Color.red;
    }
}

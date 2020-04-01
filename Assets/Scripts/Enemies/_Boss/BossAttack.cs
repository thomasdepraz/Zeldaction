using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttack : MonoBehaviour
{
    public int attackDamage = 3;
    private float attackRange = 0.45f;
    public LayerMask attackMask;
    public GameObject leftClaw;
    public GameObject rightClaw;
    public bool canDoDamage = true;
    public float bossCD = 1f;
    [Range (0,10)]
    public float knockbackforce = 7f;
    [Range (0,1)]
    public float knockbackduration = 0.3f;
    public GameObject player;

    public void Attack(float knockbackForce, float attackRange, float knockbackDuration, int attackDamage, GameObject player)
    {
        Vector3 lcpos = leftClaw.transform.position;
        Vector3 rcpos = rightClaw.transform.position;
        Collider2D lColInfo = Physics2D.OverlapCircle(lcpos, attackRange, attackMask);
        Collider2D rColInfo = Physics2D.OverlapCircle(rcpos, attackRange, attackMask);


        if (lColInfo != null && canDoDamage == true)
        {
            player.GetComponent<PlayerHP>().TakeDamage(attackDamage);
            Knockback(player.gameObject, knockbackForce);
            StartCoroutine(KnockBackMove(player.GetComponent<PlayerMovement>(), knockbackDuration));
            StartCoroutine(BossCD(bossCD));
        }
        else if (rColInfo != null && canDoDamage == true)
        {
            rColInfo.GetComponent<PlayerHP>().TakeDamage(attackDamage);
            Knockback(player.gameObject, knockbackForce);
            StartCoroutine(KnockBackMove(player.GetComponent<PlayerMovement>(), knockbackDuration));
            StartCoroutine(BossCD(bossCD));
        }
    }
   void Knockback(GameObject player, float force)
    {
        Vector2 direction = (Vector2)(player.transform.position - gameObject.transform.position); //direction du knockback
        player.GetComponent<Rigidbody2D>().velocity = (direction.normalized * force); //applique la direction et la force au knockback au RB de l'ennemi
    }

    IEnumerator KnockBackMove(PlayerMovement player, float duration)
    {
        player.canMove = false;
        yield return new WaitForSeconds(duration);
        player.canMove = true;
    }
    IEnumerator BossCD(float bossCD)
    {
        canDoDamage = false;
        yield return new WaitForSeconds(bossCD);
        canDoDamage = true;
    }
    /* void OnDrawGizmosSelected()
     {
         if (leftClaw.transform == null)
             return;
     }
         Gizmos.DrawWireSphere(leftClaw.transform.position , attackRange);*/

    // Update is called once per frame
    void Update()
    {
        Attack(knockbackforce, attackRange, knockbackduration, attackDamage, player);
    }
    void Start()
    {
        // playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
    }
}

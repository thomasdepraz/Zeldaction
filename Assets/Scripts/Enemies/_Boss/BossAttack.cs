using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttack : MonoBehaviour
{
    public int attackDamage = 3;
    public float attackRange = 0.3f;
    public LayerMask attackMask;
    public GameObject leftClaw;
    public GameObject rightClaw;
    public bool canDoDamage = true;
    public float bossCD = 1f;
    //public GameObject rightClaw;

    public void Attack()
    {
        Vector3 lcpos = leftClaw.transform.position;
        Vector3 rcpos = rightClaw.transform.position;

        Collider2D lColInfo = Physics2D.OverlapCircle(lcpos, attackRange, attackMask);
        Collider2D rColInfo = Physics2D.OverlapCircle(rcpos, attackRange, attackMask);

        if (lColInfo != null && canDoDamage == true)
        {
            lColInfo.GetComponent<PlayerHP>().TakeDamage(attackDamage);
            StartCoroutine(BossCD());
        }
        else if (rColInfo != null && canDoDamage == true)
        {
            rColInfo.GetComponent<PlayerHP>().TakeDamage(attackDamage);
            StartCoroutine(BossCD());
        }
    }
        IEnumerator BossCD()
        {
        canDoDamage = false;
        yield return new WaitForSeconds(bossCD);
        canDoDamage = true;
        }
    void OnDrawGizmosSelected()
    {
        if (leftClaw.transform == null)
            return;
        Gizmos.DrawWireSphere(leftClaw.transform.position , attackRange);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Attack();
    }
}

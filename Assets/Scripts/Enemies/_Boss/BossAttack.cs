using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttack : MonoBehaviour
{
    public int attackDamage = 3;
    public Vector3 attackOffset;
    public float attackRange = 0.4f;
    public LayerMask attackMask;
    public GameObject leftClaw;
    public bool canTakeDamage = true;
    public float bossCD = 1f;
    //public GameObject rightClaw;

    public void Attack()
    {
        Vector3 lcpos = leftClaw.transform.position;
        lcpos += transform.right * attackOffset.x;
        lcpos += transform.up * attackOffset.y;

        Collider2D colInfo = Physics2D.OverlapCircle(lcpos, attackRange, attackMask);
        if (colInfo != null && canTakeDamage == true)
        {
            colInfo.GetComponent<PlayerHP>().TakeDamage(attackDamage);
            StartCoroutine(BossCD());
        }
    }
        IEnumerator BossCD()
        {
        canTakeDamage = false;
        yield return new WaitForSeconds(bossCD);
        canTakeDamage = true;
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

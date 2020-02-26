using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [Header ("Attack Details:")]
    public Transform attackPoint;
    [Range(0f,0.1f)]
    public float attackRange = 0.05f;
    public float attackRate = 2f;
    float nextAttackTime = 0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= nextAttackTime)
        {
            if (Input.GetButton("AttackButton"))
            {
                Attack();
                nextAttackTime = Time.time + 1f / attackRate;
                Debug.Log("j'attaque!");
            }
        }
    }
    void Attack()
    {
        Collider2D[] hit = Physics2D.OverlapCircleAll(attackPoint.position, attackRange);
        foreach(Collider2D enemy in hit)
        {
            Debug.Log("touché!");
        }
    }
    void OnDrawGizmosSelected()
    {
        if (attackPoint==null)
            return;
        
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}

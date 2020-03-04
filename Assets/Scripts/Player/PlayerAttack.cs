using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [Header("Attack Details:")]
    public Transform attackPoint;
    [Range(0f, 100f)]
    public int attackDamage = 40;
    [Range(0f, 0.1f)]
    public float attackRange = 0.05f;
    public float attackRate = 2f;
    float nextAttackTime = 0f;

    [Header("Knockback")]
    [Range(0f, 0.001f)]
    public float knockbackForce = 0.001f;
    public Transform enemyPos;

    public LayerMask enemyLayer;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= nextAttackTime)
        {
            if (Input.GetButtonDown("AttackButton"))
            {
                Debug.Log("j'attaque!");
                Attack();
                nextAttackTime = Time.time + 1f / attackRate;
            }
        }
    }
    void Attack()
    {
        //play animation
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer);
        foreach (Collider2D enemy in hitEnemies)
        {
            if (enemy.GetType() == typeof(BoxCollider2D)) //va prendre en compte uniquement les boxcollider de l'ennemi dans le calcul des dommages
            {
                enemy.GetComponent<EnemyHP>().TakeDamage(attackDamage);
                //enemy.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic; //lui donne un RB dynamic, ce qui lui permet d'être knockedback
                enemy.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None; //dé-lock sa position
                Knockback(enemy.gameObject);
                Debug.Log("touché!");
            }
        }
    }

    void Knockback(GameObject enemy)
    {
        //enemy.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
        Vector2 direction = (Vector2)(enemy.transform.position - gameObject.transform.position); //direction du knockback
        enemy.GetComponent<Rigidbody2D>().velocity = (direction.normalized * knockbackForce) / 100; //applique la direction et la force au knockback au RB de l'ennemi
        Debug.Log("kb fonctionne");
    }
    void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
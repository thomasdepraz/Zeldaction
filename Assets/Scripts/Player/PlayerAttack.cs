using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [Header("Attack Details:")]
    public Transform heavyAttackPoint;
    public Transform lightAttackPoint;
    [Range(0f, 100f)]
    public int lightAttackDamage = 40;
    [Range(0f, 150f)]
    public int heavyAttackDamage = 80;
    [Range(0f, 0.1f)]
    public float attackRange = 0.5f;
    public float attackRate = 2f;
    float nextAttackTime = 0f;
    public float startTime;

    [Header("Knockback")]
    [Range(0f, 0.003f)]
    public float lightKnockbackForce = 0.001f;
    [Range(0f, 0.005f)]
    public float heavyKnockbackForce = 0.003f;
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
            if (Input.GetButton("AttackButton"))
            {
                //charge l'attaque
                startTime += Time.deltaTime;
            }
            if (Input.GetButtonUp("AttackButton") && startTime < 2f) // si le bouton est pressé moins de deux secondes, fait une attaque rapide
            {
                Debug.Log(startTime);
                LightAttack();
                nextAttackTime = Time.time + 1f / attackRate;
                startTime = 0;
            }
            if (Input.GetButtonUp("AttackButton") && startTime > 2f) // si pressé pendant plus de deux secondes, fait uen attaque lourde
            {
                Debug.Log(startTime);
                HeavyAttack();
                nextAttackTime = Time.time + 1f / attackRate;
                startTime = 0;
            }
        }
    }
    void LightAttack()
    {
        //play animation
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(lightAttackPoint.position, attackRange, enemyLayer);
        foreach (Collider2D enemy in hitEnemies)
        {
            if (enemy.GetType() == typeof(BoxCollider2D)) //va prendre en compte uniquement les boxcollider de l'ennemi dans le calcul des dommages
            {
                enemy.GetComponent<EnemyHP>().TakeDamage(lightAttackDamage);
                //enemy.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic; //lui donne un RB dynamic, ce qui lui permet d'être knockedback
                enemy.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None; //dé-lock sa position
                LightKnockback(enemy.gameObject);
                Debug.Log("touché rapide!");
            }
        }
    }

    void HeavyAttack()
    {
        //play animation
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(heavyAttackPoint.position, attackRange, enemyLayer);
        foreach (Collider2D enemy in hitEnemies)
        {
            if (enemy.GetType() == typeof(BoxCollider2D)) //va prendre en compte uniquement les boxcollider de l'ennemi dans le calcul des dommages
            {
                enemy.GetComponent<EnemyHP>().TakeDamage(heavyAttackDamage);
                enemy.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None; //dé-lock sa position
                HeavyKnockback(enemy.gameObject);
                Debug.Log("touché lourd!");
            }
        }
    }
    void LightKnockback(GameObject enemy)
    {
        //enemy.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
        Vector2 direction = (Vector2)(enemy.transform.position - gameObject.transform.position); //direction du knockback
        enemy.GetComponent<Rigidbody2D>().velocity = (direction.normalized * lightKnockbackForce) / 100; //applique la direction et la force au knockback au RB de l'ennemi
    }
    void HeavyKnockback(GameObject enemy)
    {
        Vector2 direction = (Vector2)(enemy.transform.position - gameObject.transform.position); //direction du knockback
        enemy.GetComponent<Rigidbody2D>().velocity = (direction.normalized * heavyKnockbackForce) / 100; //applique la direction et la force au knockback au RB de l'ennemi
    }
    void OnDrawGizmosSelected()
    {
        if (lightAttackPoint == null)
            return;

        Gizmos.DrawWireSphere(lightAttackPoint.position, attackRange);
    }
}
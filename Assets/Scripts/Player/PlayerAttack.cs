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
    [Range(0f, 3f)]
    public float lightKnockbackForce = 1f;
    [Range(0f, 5f)]
    public float heavyKnockbackForce = 2f;
    float lightKnockbackDuration = 2f;
    float heavyKnockbackDuration = 4f;
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
            if (Input.GetButtonUp("AttackButton") && startTime < 1f) // si le bouton est pressé moins de une seconde, fait une attaque rapide
            {
                Attack(lightAttackDamage, lightKnockbackForce, lightKnockbackDuration);
                nextAttackTime = Time.time + 1f / attackRate;
                startTime = 0;
            }
            if (Input.GetButtonUp("AttackButton") && startTime > 1f) // si pressé pendant plus de une seconde, fait uen attaque lourde
            {
                Attack(heavyAttackDamage, heavyKnockbackForce, heavyKnockbackDuration);
                nextAttackTime = Time.time + 1f / attackRate;
                startTime = 0;
            }
        }
    }
    void Attack(int attackDamage, float knockbackForce, float knockbackDuration)
    {
        //play animation
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(lightAttackPoint.position, attackRange, enemyLayer);
        foreach (Collider2D enemy in hitEnemies)
        {
            if (enemy.GetType() == typeof(BoxCollider2D)) //va prendre en compte uniquement les boxcollider de l'ennemi dans le calcul des dommages
            {
                enemy.GetComponent<EnemyHP>().TakeDamage(attackDamage);
                StartCoroutine(KnockBackMove(enemy.GetComponent<EnemyMovement>(), knockbackDuration));
                //enemy.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic; //lui donne un RB dynamic, ce qui lui permet d'être knockedback
                enemy.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None; //dé-lock sa position
                Knockback(enemy.gameObject, knockbackForce);
                Debug.Log(attackDamage);
            }
        }
    }
    void Knockback(GameObject enemy, float force)
    {
        Vector2 direction = (Vector2)(enemy.transform.position - gameObject.transform.position); //direction du knockback
        enemy.GetComponent<Rigidbody2D>().velocity = (direction.normalized * force); //applique la direction et la force au knockback au RB de l'ennemi
    }
    void OnDrawGizmosSelected()
    {
        if (lightAttackPoint == null)
            return;

        Gizmos.DrawWireSphere(lightAttackPoint.position, attackRange);
    }

    IEnumerator KnockBackMove(EnemyMovement enemy, float knockbackDuration)
    {
        enemy.canMove = false;
        yield return new WaitForSeconds(knockbackDuration);
        enemy.canMove = true;
    }
}
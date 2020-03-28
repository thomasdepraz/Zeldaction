using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [Header("Attack Details:")]
    public Transform heavyAttackPoint;
    public Transform lightAttackPoint;
    [Range(0f, 3f)]
    public int lightAttackDamage = 1;
    [Range(0f, 5f)]
    public int heavyAttackDamage = 2;
    public float lightAttackRange = 0.3f;
    public float heavyAttackRange = 0.5f;
    private bool canAttack = true;
    public float lightAttackCooldown = 0.2f;
    public float heavyAttackCooldown = 1f;
    public float channelTime;

    [Header("Knockback")]
    [Range(0f, 3f)]
    public float lightKnockbackForce = 1f;
    [Range(0f, 5f)]
    public float heavyKnockbackForce = 2f;
    [Range(0f, 5f)]
    public float lightKnockbackDuration = 2f;
    [Range(0f, 5f)]
    public float heavyKnockbackDuration = 4f;

    [Header("Layers")]
    public LayerMask enemyLayer;
    public LayerMask propsLayer;

    private HookThrow hookThrow;
    private GameObject hook;
    private void Start()
    {
        hook = GameObject.FindGameObjectWithTag("Hook");
        hookThrow = gameObject.GetComponent<HookThrow>();
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("AttackButton"))
        {
            //charge l'attaque
            channelTime += Time.deltaTime;
            if (channelTime > 0.3f)
            {
                GetComponent<PlayerMovement>().playerSpeed = 140f; // ralentit le player quand il canalise l'attaque lourde
            }
        }
        if (Input.GetButtonUp("AttackButton") && canAttack == true && channelTime < 1f) // si le bouton est pressé moins de 1 secondes et que le cd est respecté, fait une attaque rapide
        {
            Attack(lightAttackDamage, lightKnockbackForce, lightKnockbackDuration, lightAttackRange);
            StartCoroutine(AttackCooldown(lightAttackCooldown));
            channelTime = 0;
            GetComponent<PlayerMovement>().playerSpeed = 200f;
        }
        if (Input.GetButtonUp("AttackButton") && canAttack == true && channelTime > 1f) // si pressé pendant plus de 1 secondes, fait uen attaque lourde
        {
            Attack(heavyAttackDamage, heavyKnockbackForce, heavyKnockbackDuration, heavyAttackRange);
            StartCoroutine(AttackCooldown(heavyAttackCooldown));
            channelTime = 0;
            GetComponent<PlayerMovement>().playerSpeed = 200f;
        }
    }
    void Attack(int attackDamage, float knockbackForce, float knockbackDuration, float attackRange)
    {
        //play animation
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(lightAttackPoint.position, attackRange, enemyLayer);
        foreach (Collider2D enemy in hitEnemies)
        {
            if (enemy.GetType() == typeof(BoxCollider2D)) //va prendre en compte uniquement les boxcollider de l'ennemi dans le calcul des dommages
            {
                enemy.GetComponent<EnemyHP>().TakeDamage(attackDamage);
                StartCoroutine(KnockBackMove(enemy.GetComponent<EnemyMovement>(), knockbackDuration));
                enemy.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None; //dé-lock sa position
                //enemy.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;

                Knockback(enemy.gameObject, knockbackForce);
                Debug.Log(attackDamage);
                Debug.Log(channelTime);
            }
        }

        Collider2D[] hitProps = Physics2D.OverlapCircleAll(lightAttackPoint.position, attackRange, propsLayer);
        foreach (Collider2D props in hitProps)
        {
            if(props.GetType() == typeof(BoxCollider2D))
            {
                if(hook.transform.parent.gameObject.name == props.gameObject.name)
                {
                    Debug.Log("J'ai tapé");
                    props.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                    hookThrow.isHooked = false;
                    hookThrow.Pull();
                }
                Knockback(props.gameObject, knockbackForce * 5);

                if(props.CompareTag("Rock"))
                {
                    if(attackDamage == lightAttackDamage)
                        props.gameObject.GetComponent<DestroyableRocks>().LightHit();
                    else
                        props.gameObject.GetComponent<DestroyableRocks>().HeavyHit();

                }
            }
        }
    }
    void Knockback(GameObject enemy, float force)
    {
        Vector2 direction = (Vector2)(enemy.transform.position - gameObject.transform.position); //direction du knockback
        enemy.GetComponent<Rigidbody2D>().velocity = (direction.normalized * force); //applique la direction et la force au knockback au RB de l'ennemi
    }
    IEnumerator KnockBackMove(EnemyMovement enemy, float knockbackDuration)
    {
        enemy.canMove = false;
        yield return new WaitForSeconds(knockbackDuration);
        enemy.canMove = true;
    }
    IEnumerator AttackCooldown(float attackCooldown)
    {
        canAttack = false;
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
    }
    void OnDrawGizmosSelected()
    {
        if (lightAttackPoint == null)
            return;

        Gizmos.DrawWireSphere(lightAttackPoint.position, lightAttackRange);
        Gizmos.DrawWireSphere(heavyAttackPoint.position, heavyAttackRange);
    }
}
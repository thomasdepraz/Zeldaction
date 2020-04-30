using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [Header("Attack Details:")]
    public Transform heavyAttackPoint;
    public Transform lightAttackPoint;
    public Collider2D heavyAttackPointCollider;
    public Collider2D lightAttackPointCollider;
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
    private ContactFilter2D enemyFilter;
    private ContactFilter2D propsFilter;
    private ContactFilter2D bossFilter;

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
    public LayerMask bossLayer;

    private HookThrow hookThrow;
    private GameObject hook;

    private Animator anim;
    private void Start()
    {
        hook = GameObject.FindGameObjectWithTag("Hook");
        hookThrow = gameObject.GetComponent<HookThrow>();
        anim = gameObject.GetComponent<Animator>();

        enemyFilter.useLayerMask = true;
        enemyFilter.layerMask = enemyLayer;
        propsFilter.useLayerMask = true;
        propsFilter.layerMask = propsLayer;
        propsFilter.useTriggers = true;
        bossFilter.useLayerMask = true;
        bossFilter.layerMask = bossLayer;
    }
    // Update is called once per frame
    void Update()
    {
        if(PlayerManager.canAttack)
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
                anim.SetBool("isAttacking", true);

                Attack(lightAttackDamage, lightKnockbackForce, lightKnockbackDuration, lightAttackRange, lightAttackPointCollider);
                StartCoroutine(AttackCooldown(lightAttackCooldown));
                channelTime = 0;
                GetComponent<PlayerMovement>().playerSpeed = 200f;
            }
            if (Input.GetButtonUp("AttackButton") && canAttack == true && channelTime > 1f) // si pressé pendant plus de 1 secondes, fait uen attaque lourde
            {
                anim.SetBool("isHeavyAttack", true);
                Attack(heavyAttackDamage, heavyKnockbackForce, heavyKnockbackDuration, heavyAttackRange, heavyAttackPointCollider);
                StartCoroutine(AttackCooldown(heavyAttackCooldown));
                channelTime = 0;
                GetComponent<PlayerMovement>().playerSpeed = 200f;
            }
        }
    }
    void Attack(int attackDamage, float knockbackForce, float knockbackDuration, float attackRange, Collider2D collider)
    {

        List<Collider2D> hitEnemies = new List<Collider2D>();
        Physics2D.OverlapCollider(collider, enemyFilter, hitEnemies);
        foreach (Collider2D enemy in hitEnemies)
        {
            if (enemy.GetType() == typeof(BoxCollider2D)) //va prendre en compte uniquement les boxcollider de l'ennemi dans le calcul des dommages
            {
                enemy.GetComponent<EnemyHP>().TakeDamage(attackDamage);
                StartCoroutine(KnockBackMove(enemy.GetComponent<EnemyMovement>(), knockbackDuration));
                enemy.GetComponent<Rigidbody2D>().constraints &= ~RigidbodyConstraints2D.FreezePositionX;
                enemy.GetComponent<Rigidbody2D>().constraints &= ~RigidbodyConstraints2D.FreezePositionY;
                //dé-lock sa position
                //enemy.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;

                Knockback(enemy.gameObject, knockbackForce);
            }
        }

        List<Collider2D> hitBoss = new List<Collider2D>();
        Physics2D.OverlapCollider(collider, bossFilter, hitBoss);
        foreach (Collider2D boss in hitBoss)
        {
            if (boss.GetType() == typeof(BoxCollider2D)) //va prendre en compte uniquement les boxcollider de l'ennemi dans le calcul des dommages
            {
                boss.GetComponent<BossHP>().TakeDamage(attackDamage);
            }
        }

        List<Collider2D> hitProps = new List<Collider2D>();
        Physics2D.OverlapCollider(collider, propsFilter, hitProps);
        foreach (Collider2D props in hitProps)
        {
            Debug.Log(props.gameObject.name);
            if(props.GetType() == typeof(BoxCollider2D))
            {
                if (hook.transform.parent.gameObject.name == props.gameObject.name)
                {
                    Debug.Log("J'ai tapé");
                    props.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                    hookThrow.isHooked = false;
                    hookThrow.Pull();
                }
                Knockback(props.gameObject, knockbackForce * 5);

                if(props.CompareTag("Rock") && hitProps.Count <= 1)
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
        anim.SetBool("isAttacking", false);
        anim.SetBool("isHeavyAttack", false);
    }
    void OnDrawGizmosSelected()
    {
        if (lightAttackPoint == null)
            return;

        Gizmos.DrawWireSphere(lightAttackPoint.position, lightAttackRange);
        Gizmos.DrawWireSphere(heavyAttackPoint.position, heavyAttackRange);
    }
}
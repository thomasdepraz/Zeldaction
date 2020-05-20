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
    private ContactFilter2D bossLegFilter;

    private EnemyHP enemyHP;

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
    public LayerMask bossLegLayer;

    private HookThrow hookThrow;
    public GameObject hook;

    private Animator anim;
    public PlayerAudioManager playerAudio;
    private void Start()
    {
        hookThrow = gameObject.GetComponent<HookThrow>();
        anim = gameObject.GetComponent<Animator>();

        enemyFilter.useLayerMask = true;
        enemyFilter.layerMask = enemyLayer;
        propsFilter.useLayerMask = true;
        propsFilter.layerMask = propsLayer;
        propsFilter.useTriggers = true;
        bossFilter.useLayerMask = true;
        bossFilter.layerMask = bossLayer;
        bossLegFilter.useLayerMask = true;
        bossLegFilter.layerMask = bossLegLayer;
        bossLegFilter.useTriggers = true;
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
                playerAudio.PlayClip(playerAudio.lightAttack, 1, playerAudio.attack);
                //StartCoroutine(AttackCooldown(lightAttackCooldown));
                StartCoroutine("resetAttack");
                channelTime = 0;
                GetComponent<PlayerMovement>().playerSpeed = 150f;
                canAttack = false;
            }
            if (Input.GetButtonUp("AttackButton") && canAttack == true && channelTime > 1f) // si pressé pendant plus de 1 secondes, fait uen attaque lourde
            {
                anim.SetBool("isHeavyAttack", true);
                Attack(heavyAttackDamage, heavyKnockbackForce, heavyKnockbackDuration, heavyAttackRange, heavyAttackPointCollider);
                playerAudio.PlayClip(playerAudio.heavyAttack, 1, playerAudio.attack);
                //StartCoroutine(AttackCooldown(heavyAttackCooldown));
                StartCoroutine("resetAttack");
                channelTime = 0;
                GetComponent<PlayerMovement>().playerSpeed = 150f;
                canAttack = false;
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
                if (enemy.TryGetComponent<EnemyHP>(out enemyHP))
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
            if (props.GetType() == typeof(BoxCollider2D))
            {
                if (hook.transform.parent.gameObject.name == props.gameObject.name)
                {
                    props.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                    hookThrow.isHooked = false;
                    hookThrow.Pull();
                }
                Knockback(props.gameObject, knockbackForce * 5);

                if(props.CompareTag("Rock"))
                {
                    if (attackDamage == lightAttackDamage)
                        props.gameObject.GetComponent<DestroyableRocks>().LightHit();
                    else
                        props.gameObject.GetComponent<DestroyableRocks>().HeavyHit();

                }
            }
        }
        List<Collider2D> hitBossLeg = new List<Collider2D>();
        Physics2D.OverlapCollider(collider, bossLegFilter, hitBossLeg);
        foreach (Collider2D bossLeg in hitBossLeg)
        {
            Debug.Log(bossLeg.gameObject.name);
            if (bossLeg.GetType() == typeof(BoxCollider2D))
            {
                bossLeg.GetComponent<LegHP>().TakeDamage(attackDamage);
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

    public void GetAnimationEvent(string parameter)
    {
        if(parameter == "HAEnded")
        {
            //anim.SetBool("isAttacking", false);
            //anim.SetBool("isHeavyAttack", false);
            //canAttack = true;
        }

        if (parameter == "LAEnded")
        {
            //anim.SetBool("isAttacking", false);
            //anim.SetBool("isHeavyAttack", false);
            //canAttack = true;
        }
    }
    void OnDrawGizmosSelected()
    {
        if (lightAttackPoint == null)
            return;

        Gizmos.DrawWireSphere(lightAttackPoint.position, lightAttackRange);
        Gizmos.DrawWireSphere(heavyAttackPoint.position, heavyAttackRange);
    }
    
    private IEnumerator resetAttack()
    {
        yield return new WaitForSeconds(0.4f);
        canAttack = true;
        anim.SetBool("isAttacking", false);
        anim.SetBool("isHeavyAttack", false);
    }
}
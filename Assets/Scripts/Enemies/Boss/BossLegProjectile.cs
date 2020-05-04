using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossLegProjectile : MonoBehaviour
{

    [Header("Elements")]
    public Animator anim;

    [Header("Logic")]
    public int projectileDamage;

    [Header("Tweaks")]
    [Range(0, 5)]
    public float strikeRange;


    private GameObject player;
    public float startTimer;
    public float timer;
    public float brokenTimer;
    private bool LegHit;
    private bool Missed;
    public bool Broken;
    public LayerMask bossMask;
    private float attackRange = 0.47f;




    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        LegHit = false;
        Missed = false;
        Broken = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Missed == true)
        {
            if (Broken == false)
            {
                timer = startTimer += Time.deltaTime;
            }
            else
            {
                timer = brokenTimer += Time.deltaTime;
            }

            if (timer >= 10f)
            {
                Destroy(gameObject);
                BossLegThrow.Instance.LegCounter--;
                GameObject.Find("Boss").GetComponent<BossLegThrow>().canRecall = true;
            }

        }
        LegAttack(attackRange);
    }

    public void Explode()
    {
        //Lancer l'anim d'explo
        if ((player.transform.position - gameObject.transform.position).magnitude <= strikeRange)
        {
            GameObject.Find("Boss").GetComponent<BossLegThrow>().hasHit = true;
            player.GetComponent<PlayerHP>().TakeDamage(projectileDamage);
            LegHit = true;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, strikeRange);
        Gizmos.color = Color.red;
    }

    public void GetAnimationEvent(string parameter)
    {
        if (parameter == "explode")
        {
            anim.SetBool("Explode", true);
            Explode();
        }
        if (parameter == "endExplosion")
        {
            if (LegHit == true)
            {
                Destroy(gameObject);
                BossLegThrow.Instance.LegCounter--;
            }
            else
            {
                gameObject.GetComponent<BoxCollider2D>().enabled = true;
                startTimer = 0f;
                Missed = true;
            }
        }
    }
    public void LegAttack(float attackRange)
    {
        Vector3 pos = transform.position;
        Collider2D ColInfo = Physics2D.OverlapCircle(pos, attackRange, bossMask);

        if (ColInfo != null)
        {
            Debug.Log("T'as mourru le boss");
        }
    }
}
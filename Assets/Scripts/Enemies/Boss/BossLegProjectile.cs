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
    private bool canPlayBrokenSound = true;
    public LayerMask bossMask;
    private float attackRange = 0.47f;
    public SpriteRenderer sr;
    public BossAudioManager bossAudio;


    void Awake()
    {
        bossAudio = GameObject.Find("BossAudioSource").GetComponent<BossAudioManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        sr = BossLegThrow.Instance.sr;
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
            else if (Broken == true)
            {
                timer = brokenTimer += Time.deltaTime;
                if (canPlayBrokenSound == true)
                {
                    bossAudio.PlayClipNat(bossAudio.soundSource, bossAudio.PatteCassée, 1, bossAudio.health);
                    canPlayBrokenSound = false;
                }
            }

            if (BossLegThrow.Instance.isInjured == false)
            {
                if (timer >= 8f)
                {
                    BossLegThrow.Instance.canRecall = true;
                    BossLegThrow.Instance.timer = true;
                }
            }

            else if (BossLegThrow.Instance.isInjured == true)
            {
                if (timer >= 3f)
                {
                    BossLegThrow.Instance.canRecall = true;
                    BossLegThrow.Instance.timer = true;
                }
            }

        }
        LegAttack(attackRange);
    }

    public void Explode()
    {
        bossAudio.PlayClip(bossAudio.soundSource, bossAudio.FrappePatte, 1, bossAudio.attack);
        //Lancer l'anim d'explo
        if ((player.transform.position - gameObject.transform.position).magnitude <= strikeRange)
        {
            BossLegThrow.Instance.hasHit = true;
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
                BossLegThrow.Instance.canRecall = true;
                GetComponent<SpriteRenderer>().enabled = false;
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
            if (BossLegThrow.Instance.isInjured == false)
            {
                bossAudio.PlayClipNat(bossAudio.soundSource, bossAudio.PriseDégats, 1, bossAudio.health);
                StartCoroutine(DamageFB());
                GetComponent<SpriteRenderer>().enabled = false;
            }
            else
            {
                StartCoroutine(DamageFB());
                GetComponent<SpriteRenderer>().enabled = false;
                BossManager.deadBoss = true;
                // déclencher l'animation de mort
            }
        }
    }
    public IEnumerator DamageFB()
    {
        sr.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        sr.color = Color.white;
        yield return new WaitForSeconds(0.1f);
        sr.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        sr.color = Color.white;
        GameObject[] legs = GameObject.FindGameObjectsWithTag("Hookable");
        foreach (GameObject projectileLeg in legs)
        {
            BossLegThrow.Instance.LegCounter--;
            GameObject.Destroy(projectileLeg);
            if (BossLegThrow.Instance.isInjured == false)
            {
                BossLegThrow.Instance.isInjured = true;
            }
        }
    }
}
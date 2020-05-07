using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossLegThrow : MonoBehaviour
{
    public static BossLegThrow Instance;
    public GameObject player;
    public GameObject projectileLeg;
    public GameObject LeftLeg;
    public GameObject RightLeg;
    private bool isImpair; //0 = left leg, 1 = right leg
    public bool canThrow;
    public bool canRecall;
    public bool isInjured;
    public Animator anim;
    public bool hasHit;
    public int LegCounter;
    public bool timer;
    public SpriteRenderer sr;

    void Awake()
    {
        Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        canThrow = GetComponent<BossManager>().canThrow;
        canRecall = false;
        hasHit = false;
        timer = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isInjured == false)
        {
            if (LegCounter == 2 && timer == true)
            {
                canRecall = true;
                //timer = false;
                anim.SetTrigger("canRecall");
                canRecall = false;
            }

            else if (canThrow == true && LegCounter < 2)
            {
                if (isImpair == false)
                {
                    anim.SetBool("isImpair", false);
                }
                else if (isImpair == true)
                {
                    anim.SetBool("isImpair", true);
                }
                anim.SetTrigger("Throw");
                canThrow = false;
                timer = false;
                StartCoroutine(ThrowCD());
            }
        }

        else if (isInjured == true)
        {
            if (canThrow == true && LegCounter < 1)
            {
                if (isImpair == false)
                {
                    anim.SetBool("isImpair", false);
                }
                else if (isImpair == true)
                {
                    anim.SetBool("isImpair", true);
                }
                anim.SetTrigger("Throw");
                canThrow = false;
                StartCoroutine(ThrowCD());
            }

            if (canRecall == true && LegCounter == 1)
            {
                anim.SetTrigger("canRecall");
                canRecall = false;
            }
        }


        if (hasHit == true)
        {
            GameObject[] legs = GameObject.FindGameObjectsWithTag("Hookable");
            foreach (GameObject projectileLeg in legs)
            {
                GameObject.Destroy(projectileLeg);
                LegCounter--;
            }
            anim.SetTrigger("Hit");
            canRecall = true;
            hasHit = false;
        }

    }
    void Throw()
    {
        GameObject Projectile = Instantiate(projectileLeg, transform.position, Quaternion.identity);
        Projectile.transform.position = player.transform.position;
        if (isInjured == false)
        {
            if (isImpair == false)
            {
                LeftLeg.SetActive(false);
                isImpair = true;
                LegCounter++;
            }
            else
            {
                RightLeg.SetActive(false);
                isImpair = false;
                LegCounter++;
            }
        }
        else
        {
            if (isImpair == false)
            {
                LeftLeg.SetActive(false);
                LegCounter++;
            }
            else
            {
                RightLeg.SetActive(false);
                LegCounter++;
            }
        }
        anim.ResetTrigger("Throw");
    }

    public void Recall()
    {
        GameObject[] legs = GameObject.FindGameObjectsWithTag("Hookable");
        foreach (GameObject projectileLeg in legs)
        {
            GameObject.Destroy(projectileLeg);
            LegCounter--;
        }

        anim.ResetTrigger("canRecall");
        LeftLeg.SetActive(true);
        RightLeg.SetActive(true);
        StartCoroutine(ThrowCD());
        canRecall = false;
    }
    public IEnumerator ThrowCD() // a utiliser dans l'animator en tant qu'animation event
    {
        yield return new WaitForSeconds(0.5f);
        canThrow = true;
    }
}

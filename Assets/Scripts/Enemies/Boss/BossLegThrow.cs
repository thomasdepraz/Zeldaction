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
    private bool isInjured;
    public Animator anim;
    public bool hasHit;
    public int LegCounter;

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
    }

    // Update is called once per frame
    void Update()
    {
        if (isInjured == false)
        {
            if (canThrow == true && LegCounter < 2)
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
        }
        /*else if (isInjured == true)
        {
            if (canThrow == true && LegCounter < 1)
            {
                if (isImpair == false)
                {
                    LeftLeg.SetActive(false);
                }
                else if (isImpair == true)
                {
                    RightLeg.SetActive(false);
                }
                anim.SetTrigger("Throw");
                canThrow = false;
                LegCounter++;
                StartCoroutine(ThrowCD());
            }
        }*/


        if (hasHit == true)
        {
            anim.SetTrigger("Hit");
            canRecall = true;
            hasHit = false;
        }

        if (canRecall == true)
        {
            anim.SetTrigger("canRecall");
            canRecall = false;
        }
    }
    void Throw()
    {
        LegCounter++;
        GameObject Projectile = Instantiate(projectileLeg, transform.position, Quaternion.identity);
        Projectile.transform.position = player.transform.position;
        if (isInjured == false)
        {
            if (isImpair == false)
            {
                LeftLeg.SetActive(false);
                isImpair = true;
            }
            else
            {
                RightLeg.SetActive(false);
                isImpair = false;
            }
        }
        anim.ResetTrigger("Throw");
    }
    // désactiver le game object et instancier un projectile qui ressemble au gameobject
    void Recall()
    {
        // play recall animation
        anim.ResetTrigger("canRecall");
        LeftLeg.SetActive(true);
        RightLeg.SetActive(true);
        StartCoroutine(ThrowCD());
        canRecall = false;
    }
    public IEnumerator ThrowCD() // a utiliser dans l'animator en tant qu'animation event
    {
        yield return new WaitForSeconds(3f);
        canThrow = true;
    }
    /*if (
     dans le start du projectile, il est présent depuis plus de 5 secondes
     une fois cassé, il est envoyé dans un collider autre que sur le boss, ou est présent depuis plus de 10 secondes
 */
}

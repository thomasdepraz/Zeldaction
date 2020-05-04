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
    public bool canThrow;
    public bool canRecall;
    public Animator anim;
    public bool hasHit;
    public int LegCounter = 0;

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
        if (canThrow == true && LegCounter < 3)
        {
            anim.SetTrigger("Throw");
            canThrow = false;
        }

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
        GameObject Projectile = Instantiate(projectileLeg, transform.position, Quaternion.identity);
        Projectile.transform.position = player.transform.position;
        LeftLeg.SetActive(false);
        RightLeg.SetActive(false);
        LegCounter++;
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
        yield return new WaitForSeconds(4f);
        canThrow = true;
    }
    /*if (
     dans le start du projectile, il est présent depuis plus de 5 secondes
     une fois cassé, il est envoyé dans un collider autre que sur le boss, ou est présent depuis plus de 10 secondes
 */ 
}

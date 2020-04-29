using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossLegThrow : MonoBehaviour
{
    public GameObject player;
    public GameObject projectileLeg;
    public GameObject LeftLeg;
    public GameObject RightLeg;
    public bool canThrow;
    public bool canRecall;
    public Animator anim;
    public bool hasHit = false;

    // Start is called before the first frame update
    void Start()
    {
        canThrow = GetComponent<BossManager>().canThrow;
    }

    // Update is called once per frame
    void Update()
    {
        if (canThrow == true)
        {
            anim.SetTrigger("Throw");
            canThrow = false;
        }
        if (canRecall == true)
        {
            anim.SetTrigger("Recall");
        }
        if (hasHit == true)
        {
            anim.SetBool("Hit", true);
        }
        else
        {
            anim.SetBool("Hit", false);
        }
    }
    void Throw()
    {
        GameObject Projectile = Instantiate(projectileLeg, transform.position, Quaternion.identity);
        Projectile.transform.position = player.transform.position;
        LeftLeg.SetActive(false);
        RightLeg.SetActive(false);
    }
    // désactiver le game object et instancier un projectile qui ressemble au gameobject
    void Recall()
    {
        // play recall animation
        LeftLeg.SetActive(true);
        RightLeg.SetActive(true);
        StartCoroutine(ThrowCD());
    }
    public IEnumerator ThrowCD() // a utiliser dans l'animator en tant qu'animation event
    {
        yield return new WaitForSeconds(4f);
        canThrow = true;
    }
}

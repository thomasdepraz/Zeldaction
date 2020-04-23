using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossManager : MonoBehaviour
{
    public Animator anim;
    public bool Phase1 = true;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Phase2()
    {
        anim.SetTrigger("Go_Phase2");
        Phase1 = false;
        gameObject.GetComponent<BossMovement>().enabled = false;
        gameObject.GetComponent<BossAttack>().enabled = false;
    }
}

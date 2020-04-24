using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossManager : MonoBehaviour
{
    public Animator anim;
    public bool isPhase1 = true;
    public bool isPhase2 = true;
    public GameObject plate1;
    public GameObject plate2;
    public GameObject waypoint1;
    public GameObject waypoint2;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (isPhase2 == true)
        {
        Plates();
        }
    }
    public void Phase2()
    {
        anim.SetTrigger("GoPhase2");
        isPhase1 = false;
        isPhase2 = true;
        gameObject.GetComponent<BossMovement>().enabled = false;
        gameObject.GetComponent<BossAttack>().enabled = false;
        Instantiate(plate1);
        plate1.transform.position = waypoint1.transform.position;
        Instantiate(plate2);
        plate2.transform.position = waypoint2.transform.position;

    }
    public void Plates()
    {
        if (plate1.GetComponent<PressurePlate>().isPressed && plate2.GetComponent<PressurePlate>().isPressed)
        {
            //déclenche l'anim de caillou dans la tronche du crabe
        }
    }
}

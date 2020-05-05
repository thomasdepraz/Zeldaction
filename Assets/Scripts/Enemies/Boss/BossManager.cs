using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossManager : MonoBehaviour
{
    public Animator anim;
    public bool isPhase1 = true;
    public bool isPhase2 = false;
    public bool isPhase3 = false;
    public bool canThrow;
    public bool isFinished = false;
    public GameObject plate1;
    public GameObject plate2;
    private GameObject Plate1;
    private GameObject Plate2;
    public GameObject waypoint1;
    public GameObject waypoint2;
    public GameObject stone;
    // Start is called before the first frame update
    void Start()
    {
        isPhase1 = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (isPhase2 == true)
        {
            if (Plate1.GetComponent<PressurePlate>().isPressed && Plate2.GetComponent<PressurePlate>().isPressed)
            {
                Phase3();
            }
        }
    }
    public void Phase2()
    {
        anim.SetTrigger("GoPhase2"); // intégrer l'animation de déplacement du Boss vers sa plateforme
        isPhase1 = false;
        isPhase2 = true;
        gameObject.GetComponent<BossMovement>().enabled = false;
        gameObject.GetComponent<BossAttack>().enabled = false;
        Plate1 = Instantiate(plate1, waypoint1.transform.position, Quaternion.identity);
        Plate2 = Instantiate(plate2, waypoint2.transform.position, Quaternion.identity);
    }
    public void Phase3()
    {
        Destroy(Plate1);
        Destroy(Plate2);
        stone.SetActive(true);
        anim.SetTrigger("GoPhase3"); // intégrer l'animation de transition de phase
        GetComponent<BossSummoning>().enabled = false;
        GetComponent<BossLegThrow>().enabled = true;
        isPhase3 = true;
        isPhase2 = false;
    }
}

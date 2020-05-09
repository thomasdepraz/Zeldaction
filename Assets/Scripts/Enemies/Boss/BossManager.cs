using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
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
    public GameObject WaypointBoss;
    public GameObject Anchor;
    private float bossSpeed = 15f;
    public GameObject playerCam;
    public GameObject bossCam;
    private SpriteRenderer sr;

    // Start is called before the first frame update
    void Start()
    {
        isPhase1 = true;
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isPhase2 == true)
        {
            if (Vector2.Distance(transform.position, WaypointBoss.transform.position) != 0)
            {
                float step = bossSpeed * Time.deltaTime;
                transform.position = Vector2.MoveTowards(transform.position, WaypointBoss.transform.position, step);
            }

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
        playerCam.SetActive(false);
        bossCam.SetActive(true);
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
        gameObject.GetComponent<BossAttack>().enabled = false;
        Plate1 = Instantiate(plate1, waypoint1.transform.position, Quaternion.identity);
        Plate2 = Instantiate(plate2, waypoint2.transform.position, Quaternion.identity);
        gameObject.GetComponent<BossMovement>().enabled = false;
        
    }
    public void Phase3()
    {
        Destroy(Plate1);
        Destroy(Plate2);
        Anchor.GetComponent<BrokenAnchor>().enabled = true;
        gameObject.GetComponent<BoxCollider2D>().enabled = true;
        GameObject[] monster = GameObject.FindGameObjectsWithTag("Hookable");
        foreach (GameObject Monsters in monster)
        {
            GameObject.Destroy(Monsters);
        }
        anim.SetTrigger("GoPhase3"); // intégrer l'animation de transition de phase
        GetComponent<BossSummoning>().enabled = false;
        GetComponent<BossLegThrow>().enabled = true;
        isPhase3 = true;
        isPhase2 = false;
    }
    public void AnchorFB()
    {
        StartCoroutine(AnchorDamageFB());
    }
    IEnumerator AnchorDamageFB()
    {
        sr.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        sr.color = Color.white;
        yield return new WaitForSeconds(0.1f);
        sr.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        sr.color = Color.white;
    }
}

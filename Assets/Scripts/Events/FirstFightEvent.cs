using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

public class FirstFightEvent : MonoBehaviour
{
    //Référencement du Combat à  lancer après l'Event
    public GameObject combatEvent;
    private CombatEvent fight;

    //PNJ
    public GameObject pnj;
    public GameObject pnj2;
    public GameObject pnj3;
    public Transform enterPosition;
    public Transform enterPosition02;
    public Transform enterPosition03;
    public float speed;

    //Player
    private GameObject player;
    private PlayerMovement movement;

    //Virtual Camera 
    public CinemachineVirtualCamera playerCam;
    public CinemachineVirtualCamera transitionCam;

    //Dialog
    public GameObject dialogBox;
    public Text dialogText;
    public string dialog;
    public bool dialogActive;
    public bool dialogIsFinish = false;
    private bool startedDialog = false;

    //Time to Trigger DialogBox
    public float WaitToTrigger;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        movement = player.GetComponent<PlayerMovement>();
        fight = combatEvent.GetComponent<CombatEvent>();
    }

    void Update()
    {
        if (pnj.activeInHierarchy && dialogIsFinish == false)
        {
            pnj.transform.position = Vector3.MoveTowards(pnj.transform.position, enterPosition.position, speed * Time.deltaTime);
            pnj2.transform.position = Vector3.MoveTowards(pnj2.transform.position, enterPosition02.position, speed * Time.deltaTime);
            pnj3.transform.position = Vector3.MoveTowards(pnj3.transform.position, enterPosition03.position, speed * Time.deltaTime);
        }

        if (dialogIsFinish && pnj.activeSelf)
        {
            playerCam.gameObject.SetActive(true);
            transitionCam.gameObject.SetActive(false);
            fight.combatStarted = true;
            movement.canMove = true;
            dialogBox.SetActive(false);
            pnj.SetActive(false);
            pnj2.SetActive(false);
            pnj3.SetActive(false);
            Destroy(gameObject);
        }

        if (dialogBox.activeInHierarchy && Input.GetButtonDown("interact"))
        {
            dialogIsFinish = true;
        }           
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !startedDialog)
        {
            movement.canMove = false;
            movement.playerRb.velocity = Vector2.zero;
            StartCoroutine(Interpellation());
        }
    }

    IEnumerator Interpellation()
    {
        startedDialog = true;
        yield return new WaitForSeconds(1f);
        playerCam.gameObject.SetActive(false);
        transitionCam.gameObject.SetActive(true);
        pnj.SetActive(true);
        pnj2.SetActive(true);
        pnj3.SetActive(true);
        yield return new WaitForSeconds(WaitToTrigger);
        dialogBox.SetActive(true);
        dialogText.text = dialog;
    }
}

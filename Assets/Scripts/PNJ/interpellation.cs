using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

public class interpellation : MonoBehaviour
{
    //PNJ
    public GameObject pnj;
    public Transform exitPosition;
    bool isExit = false;
    bool dialogIsFinish = false;
    public float speed;
    public float stoppingDistance;

    //Player
    private GameObject player;
    private PlayerMovement movement;
    private Transform startTarget;

    //Virtual Camera 
    public CinemachineVirtualCamera playerCam;
    public CinemachineVirtualCamera transitionCam;

    //Dialog
    public GameObject dialogBox;
    public Text dialogText;
    public string dialog;
    public bool dialogActive;

    //Feedback
    public GameObject interrogationPoint;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        movement = player.GetComponent<PlayerMovement>();
        startTarget = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    void Update()
    {
        if (pnj.activeInHierarchy && dialogIsFinish == false)
        {
            if (Vector2.Distance(pnj.transform.position, startTarget.transform.position) >= stoppingDistance)
            {
                pnj.transform.position = Vector3.MoveTowards(pnj.transform.position, startTarget.position, speed * Time.deltaTime);
            }
        }

        if (dialogBox.activeInHierarchy && Input.GetButtonDown("interact"))
        {
            playerCam.gameObject.SetActive(true);
            transitionCam.gameObject.SetActive(false);
            dialogBox.SetActive(false);
            dialogIsFinish = true;           
        }

        if (dialogIsFinish == true)
        {
            pnj.transform.position = Vector3.MoveTowards(pnj.transform.position, exitPosition.position, speed * Time.deltaTime);
            if(pnj.transform.position == exitPosition.position)
            {
                isExit = true; 
            }
        }

        if (isExit == true)
        {
            movement.canMove = true;
            Destroy(gameObject);
        }    
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            movement.canMove = false;
            interrogationPoint.SetActive(true);
            StartCoroutine(Interpellation());
        }
    }

    IEnumerator Interpellation()
    {
        yield return new WaitForSeconds(2f);
        interrogationPoint.SetActive(false);
        playerCam.gameObject.SetActive(false);
        transitionCam.gameObject.SetActive(true);
        pnj.SetActive(true);
        yield return new WaitForSeconds(1f);
        dialogBox.SetActive(true);
        dialogText.text = dialog;
    }
}

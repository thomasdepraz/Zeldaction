using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

public class DialogSystem : MonoBehaviour
{
    [Header("EventType")]
    public bool isDialog;
    public bool isForcedDialog;
    public bool isCombatEvent;

    [Header("Dialog Elements")]
    public GameObject dialogBox;
    public Text dialogText;
    public string dialog;
    public GameObject button; //USE ONLY FOR NON-FORCED DIALOG

    [Header("Forced-Dialog Elements")]
    public GameObject npc;
    public Transform npcTarget;
    public Transform npcExitTarget;
    public GameObject interrogationPoint;

    [Header("Combat Event")]
    public GameObject[] enemiesPlaceHolders;
    public Transform[] enemiesTargetPos;
    public GameObject combatEvent;
    private CombatEvent fight;

    [Header("Virtual Cameras (Forced Dialog/Combat Event)")]
    public CinemachineVirtualCamera playerCam;
    public CinemachineVirtualCamera eventCam;

    [Header("Logic")]
    [Range(0,5)]
    public int eventRange;
    public float transitionTime;
    private bool playerInRange;
    private bool eventStarted;
    private bool startMove;
    private bool dialogIsFinished;

    [Header("Objects to activate / deactivate(forced dialog only)")]
    public GameObject[] objects;
    public GameObject[] deactivateObjects;

    //Player
    private GameObject player;
    private Transform playerTransform;
    private PlayerMovement playerMovement;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerTransform = player.transform;
        playerMovement = player.GetComponent<PlayerMovement>();

        fight = combatEvent.GetComponent<CombatEvent>();
    }

    void Update()
    {
        if(playerInRange & isDialog)//THIS IS DIALOGUE CODE
        {
            if (Input.GetButtonDown("interact"))
            {
                if (dialogBox.activeInHierarchy)
                {
                    dialogBox.SetActive(false);
                }
                else
                {
                    dialogBox.SetActive(true);
                    dialogText.text = dialog;
                }
            }
        }

        if (isForcedDialog)
        {
            if(startMove && npc.transform.position != npcTarget.position)
            {
                //make npc move to npcTarget
                npc.transform.position = Vector3.MoveTowards(npc.transform.position, npcTarget.position, 1 * Time.deltaTime);
            }
            
            if(eventStarted && npc.transform.position == npcTarget.position && Input.GetButtonDown("interact"))
            {
                //deactivate dialogBox + swapcamera
                playerCam.gameObject.SetActive(true);
                eventCam.gameObject.SetActive(false);
                dialogBox.SetActive(false);
                dialogIsFinished = true;
            }

            if(dialogIsFinished)
            {
                //make npcmoveto exit pos
                npc.transform.position = Vector3.MoveTowards(npc.transform.position, npcExitTarget.position, 1 * Time.deltaTime);
            }

            if(npc.transform.position == npcExitTarget.position && dialogIsFinished)
            {
                //destroy when exit reached
                playerMovement.canMove = true;
                if(objects != null)
                {
                    for(int i =0;i < objects.Length; i++)
                    {
                        objects[i].SetActive(true);
                    }
                }
                if (deactivateObjects != null)
                {
                    for (int i = 0; i < objects.Length; i++)
                    {
                        deactivateObjects[i].SetActive(false);
                    }
                }
                Destroy(gameObject); //POUR L'INSTANT
            }
        }

        if(isCombatEvent)
        {
            //make enemies move to target positions
            if(startMove)
            {
                for (int i = 0; i < enemiesPlaceHolders.Length; i++)
                {
                    enemiesPlaceHolders[i].transform.position = Vector3.MoveTowards(enemiesPlaceHolders[i].transform.position, enemiesTargetPos[i].position, 1 * Time.deltaTime);
                }

                if(Input.GetButtonDown("interact") && eventStarted)
                {
                    dialogIsFinished = true;
                }
            }

            if(dialogIsFinished)
            {
                //deactivate dialogBox + swapcamera + start combat + deactivate  npc
                playerCam.gameObject.SetActive(true);
                eventCam.gameObject.SetActive(false);
                fight.combatStarted = true;
                playerMovement.canMove = true;
                dialogBox.SetActive(false);
                for(int i =0; i < enemiesPlaceHolders.Length; i++)
                {
                    enemiesPlaceHolders[i].SetActive(false);
                }
                Destroy(gameObject); //POUR L'INSTANT
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            if(isDialog)
            {
                playerInRange = true;
                button.SetActive(true);
            }
            else
            {
                playerMovement.canMove = false;
                playerMovement.playerRb.velocity = Vector2.zero;
                StartCoroutine("DialogEvent");
                if (isForcedDialog)
                {
                    interrogationPoint.SetActive(true);
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && isDialog)
        {
            button.SetActive(false);
            dialogBox.SetActive(false);
            playerInRange = false;
        }
    }

    IEnumerator DialogEvent()
    {
        yield return new WaitForSeconds(2f);
        if (isForcedDialog)
        {
            interrogationPoint.SetActive(false);
        }

        if(isCombatEvent)
        {
            for(int i = 0; i < enemiesPlaceHolders.Length; i++)
            {
                enemiesPlaceHolders[i].SetActive(true);
            }
        }
        playerCam.gameObject.SetActive(false);
        eventCam.gameObject.SetActive(true);
        startMove = true;
        yield return new WaitForSeconds(transitionTime);
        dialogBox.SetActive(true);
        dialogText.text = dialog;
        eventStarted = true;
    }
}

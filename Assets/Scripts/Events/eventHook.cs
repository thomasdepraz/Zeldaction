using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class eventHook : MonoBehaviour
{
    //TELEPORT
    private GameObject player;
    public Transform targetPosition;
    public GameObject transitionPanel;
    private Animator anim;
    [HideInInspector] public static bool teleport;

    //CAM TRANSITION
    public CinemachineVirtualCamera playerCam;
    public CinemachineVirtualCamera transitionCam;

    public GameObject colliderArena;
    public GameObject takableHook;
    public GameObject falseHook;

    [Header("Activable")]
    public GameObject bobInterpellation02;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        anim = transitionPanel.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (teleport)
        {
            Teleportation();
        }
    }

    public void Teleportation()
    {
        player.transform.position = targetPosition.position;
        teleport = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        transitionPanel.SetActive(true);
        anim.SetBool("teleport", false);
        anim.SetBool("teleport", true);
        colliderArena.SetActive(true);
        StartCoroutine(LoadEventHook());
    }

    IEnumerator LoadEventHook()
    {
        yield return new WaitForSeconds(2f);
        takableHook.SetActive(true);
        falseHook.SetActive(false);
        player.transform.position = targetPosition.position;
        playerCam.gameObject.SetActive(false);
        transitionCam.gameObject.SetActive(true);
        bobInterpellation02.SetActive(true);
    }
}

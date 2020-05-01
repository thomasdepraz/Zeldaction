using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    private GameObject player;
    public Transform targetPosition;
    public GameObject transitionPanel;
    private Animator anim;
    [HideInInspector] public bool teleport;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        anim = transitionPanel.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(teleport)
        {
            transitionPanel.SetActive(true);
            anim.SetBool("teleport", true);
        }
    }

    public void Teleportation()
    {
        player.transform.position = targetPosition.position;
        anim.SetBool("teleport", false);
        teleport = false;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    private GameObject player;
    public Transform targetPosition;
    public GameObject transitionPanel;
    private Animator anim;
    [HideInInspector] public static bool teleport;

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
    }
}

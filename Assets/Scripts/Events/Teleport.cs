using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Teleport : MonoBehaviour
{
    private GameObject player;
    public Transform targetPosition;
    public GameObject transitionPanel;
    private Animator anim;
    private bool canStartCoroutine = true;
    public bool dungeonTeleport;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        anim = transitionPanel.GetComponent<Animator>();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        transitionPanel.SetActive(true);
        anim.SetBool("teleport", false);
        anim.SetBool("teleport", true);
        if(canStartCoroutine)
        {
            StartCoroutine(TeleportPlayer());
        }
    }

    private IEnumerator TeleportPlayer()
    {
        canStartCoroutine = false;
        yield return new WaitForSeconds(2f);
        if (dungeonTeleport)
        {
            SceneManager.LoadScene("DungeonScene");
        }
        else
        {
            player.transform.position = targetPosition.position;
        }
        yield return new WaitForSeconds(2f);
        anim.SetBool("teleport", false);
        transitionPanel.SetActive(false);
        canStartCoroutine = true;
    }
}

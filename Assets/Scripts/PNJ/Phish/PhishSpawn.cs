using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PhishSpawn : MonoBehaviour
{
    public GameObject phish;
    private GameObject player;
    private PlayerMovement movement;

    //Dialog
    public GameObject dialogBox;
    public Text dialogText;
    public string dialog;
    public bool dialogActive;

    //Feedback
    public GameObject button;
    public GameObject interrogationPoint;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        movement = player.GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        if (dialogBox.activeInHierarchy && Input.GetButtonDown("interact"))
        {
            dialogBox.SetActive(false);
            movement.canMove = true;
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            interrogationPoint.SetActive(true);
            movement.canMove = false;
            StartCoroutine(PhishSpeak());            
        }
    }

    IEnumerator PhishSpeak()
    {
        yield return new WaitForSeconds(2f);
        interrogationPoint.SetActive(false);
        phish.SetActive(true);
        yield return new WaitForSeconds(1f);
        dialogBox.SetActive(true);
        dialogText.text = dialog;
        button.SetActive(true); 
    }
}

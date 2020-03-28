using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dialogue : MonoBehaviour
{
    public GameObject dialogBox;
    public Text dialogText;
    public string dialog;
    public bool dialogActive;
    public bool playerInRange;

    public GameObject Button;
    
    void Update()
    {
        if (Input.GetButtonDown("interact") && playerInRange)
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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            Button.SetActive(true);
        } 
    }

    private void OnTriggerExit2D(Collider2D other)
    {
       if (other.CompareTag("Player"))
        {
            Button.SetActive(false);
            dialogBox.SetActive(false);
            playerInRange = false;
        } 
    }
}

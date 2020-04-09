using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeArtefact : MonoBehaviour
{
    public bool playerInRange;
    public GameObject Button;

    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetButtonDown("interact") && playerInRange)
        {
            ArtefactTextScript.artefactCounter += 1;
            Destroy(gameObject);
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
            playerInRange = false;
        }
    }
}

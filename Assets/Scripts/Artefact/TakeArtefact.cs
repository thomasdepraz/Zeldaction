using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeArtefact : MonoBehaviour
{
    public bool playerInRange;
    public GameObject Button;
    private UIAudioManager audioManager;

    void Start()
    {
        audioManager = GetComponent<UIAudioManager>();
    }

    void Update()
    {
        if (Input.GetButtonDown("interact") && playerInRange && !audioManager.soundSource.isPlaying)
        {
            ArtefactTextScript.artefactCounter += 1;
            audioManager.PlayClip(audioManager.soundSource, audioManager.artefact,1, audioManager.artefactOutput);
            Button.GetComponent<SpriteRenderer>().enabled = false;
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            gameObject.GetComponent<Collider2D>().enabled = false;
            StartCoroutine(Artefact());

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

    private IEnumerator Artefact()
    {
        yield return new WaitUntil(() => !audioManager.soundSource.isPlaying);
        Destroy(gameObject);   
    }
}

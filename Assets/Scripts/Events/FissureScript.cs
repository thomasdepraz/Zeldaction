using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FissureScript : MonoBehaviour
{
    public GameObject fissure;
    public GameObject redEyes;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        fissure.SetActive(false);
        redEyes.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        fissure.SetActive(true);
        redEyes.SetActive(false);
    }
}

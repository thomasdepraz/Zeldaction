using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveTimeline : MonoBehaviour
{
    public GameObject timeline;
    public GameObject destroyableObjects;
    bool inRange  = false;

    private void Update()
    {
        if (inRange == true && destroyableObjects != null)
        {
            timeline.SetActive(true);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            inRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (timeline.activeInHierarchy)
        {
            Destroy(gameObject);
        }
    }

}

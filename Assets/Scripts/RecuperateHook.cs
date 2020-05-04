using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecuperateHook : MonoBehaviour
{
    bool inRange = false;
    public GameObject button;
      

    // Update is called once per frame
    void Update()
    {
        if (inRange == true && Input.GetButtonDown("interact"))
        {
            PlayerManager.hasHook = true;
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        inRange = true;
        button.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        inRange = false;
        button.SetActive(false);
    }
}

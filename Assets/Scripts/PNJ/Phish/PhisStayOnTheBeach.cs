using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhisStayOnTheBeach : MonoBehaviour
{
    private GameObject phish;
    private PhishMovement phishMove;
    public Transform pausePosition;
    public float speed;

    private void Start()
    {
        phish = GameObject.FindGameObjectWithTag("Phish");
        phishMove = phish.GetComponent<PhishMovement>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Phish"))
        {
            phishMove.canMove = false;
            phish.transform.position = Vector2.MoveTowards(phish.transform.position, pausePosition.position, speed * Time.deltaTime);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public GameObject linkedPlate;
    private PressurePlate plate;
    private BoxCollider2D col;
    // Start is called before the first frame update
    void Start()
    {
        plate = linkedPlate.GetComponent<PressurePlate>();
        col = gameObject.GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (plate.isPressed)
        {
            col.enabled = false;
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
        }
        else
        {
            col.enabled = true;
            gameObject.GetComponent<SpriteRenderer>().enabled = true;
        }
    }
}

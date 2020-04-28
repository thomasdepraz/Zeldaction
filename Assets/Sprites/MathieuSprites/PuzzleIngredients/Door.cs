using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public GameObject linkedPlate;
    public GameObject[] linkedPlates;
    private PressurePlate plate;
    private BoxCollider2D col;
    public bool stayOpen;
    // Start is called before the first frame update
    void Start()
    {
        if(linkedPlate != null)
        {
            plate = linkedPlate.GetComponent<PressurePlate>();
        }
            col = gameObject.GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(linkedPlate != null)
        {
            if (plate.isPressed)
            {
                col.enabled = false;
                gameObject.GetComponent<SpriteRenderer>().enabled = false;
            }
            else
            {
                if(!stayOpen)
                {
                    gameObject.GetComponent<SpriteRenderer>().enabled = true;
                    col.enabled = true;
                }
            }
        }

        if(linkedPlates != null)
        {
            for(int i = 0; i <= linkedPlates.Length-1; i++ )
            {
                if (!linkedPlates[i].GetComponent<PressurePlate>().isPressed)
                {
                    if(!stayOpen)
                    {
                        col.enabled = true;
                        gameObject.GetComponent<SpriteRenderer>().enabled = true;
                    }
                    break;
                }

                if(linkedPlates[i] == linkedPlates[linkedPlates.Length - 1])
                {
                    col.enabled = false;
                    gameObject.GetComponent<SpriteRenderer>().enabled = false;
                }
            }
        }
    }
}

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
    private SpriteRenderer rend;
    public Sprite closedDoor;
    public Sprite openDoor;
    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<SpriteRenderer>();
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
                rend.sortingOrder = -2;
                rend.sprite = openDoor;
                
            }
            else
            {
                if(!stayOpen)
                {
                    rend.sprite = closedDoor;
                    rend.sortingOrder = 0;
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
                        rend.sprite = closedDoor;
                        rend.sortingOrder = -2;
                       
                    }
                    break;
                }

                if(linkedPlates[i] == linkedPlates[linkedPlates.Length - 1])
                {
                    col.enabled = false;
                    rend.sprite = openDoor;
                    rend.sortingOrder = 0;
               
                }
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    public Sprite realesedSprite;
    public Sprite pressedSprite;
    public LayerMask hitboxLayer;
    private SpriteRenderer spr;
    private BoxCollider2D col;


    public bool isPressed;

    // Start is called before the first frame update
    void Start()
    {
        col = gameObject.GetComponent<BoxCollider2D>();
        spr = gameObject.GetComponent<SpriteRenderer>();
    }
    private void Update()
    {
        //Debug.Log(col.IsTouchingLayers(hitboxLayer));
    }
    private void OnTriggerEnter2D(Collider2D collision)
    { 
        if(collision.gameObject.layer == 10)
        {
            isPressed = true;
            spr.sprite = pressedSprite;
        }
        
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        
        Debug.Log(col.IsTouchingLayers(hitboxLayer));
        if(!col.IsTouchingLayers(hitboxLayer))
        {
            isPressed = false;
            spr.sprite = realesedSprite;
        }
        
        
        
    }
}

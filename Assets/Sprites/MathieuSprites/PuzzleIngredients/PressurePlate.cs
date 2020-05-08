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
    public PropsAudioManager audioManager;


    public bool isPressed;
    public bool isRigid;
    public int objectsNeeded;
    private int objectsOnPlate = 0;

    void Start()
    {
        col = gameObject.GetComponent<BoxCollider2D>();
        spr = gameObject.GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if(objectsOnPlate >= objectsNeeded && isRigid)
        {
            //audioManager.PlayClip(audioManager.onPressurePlate,1);
            isPressed = true;
            spr.sprite = pressedSprite;
        }

        else if(objectsOnPlate < objectsNeeded && isRigid)
        {
            //audioManager.PlayClip(audioManager.onPressurePlate, 1);
            isPressed = false;
            spr.sprite = realesedSprite;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    { 
        if(collision.gameObject.layer == 10 && !isRigid && !isPressed)
        {
            audioManager.PlayClip(audioManager.onPressurePlate, 1);
            isPressed = true;
            spr.sprite = pressedSprite;
        }


        if(collision.TryGetComponent(out Crate crate) && isRigid)
        {
            if(crate.isHeavyCrate || crate.isArmor)
                objectsOnPlate++;
        }
 
    }

    private void OnTriggerExit2D(Collider2D collision)
    { 
        if(!col.IsTouchingLayers(hitboxLayer) && !isRigid && isPressed)
        {
            audioManager.PlayClip(audioManager.onPressurePlate, 1);
            isPressed = false;
            spr.sprite = realesedSprite;
        }

        if (collision.gameObject.TryGetComponent(out Crate crate) && isRigid)
        {
            if (crate.isHeavyCrate || crate.isArmor)
                objectsOnPlate--;
        }           
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    public Sprite realesedSprite;
    public Sprite pressedSprite;
    private SpriteRenderer spr;

    public bool isPressed;

    // Start is called before the first frame update
    void Start()
    {
        spr = gameObject.GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        isPressed = true;
        spr.sprite = pressedSprite;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        isPressed = false;
        spr.sprite = realesedSprite;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyableRocks : MonoBehaviour
{

    public Sprite[] RockSprites = new Sprite[4];
    public int currentSpriteIndex;
    private SpriteRenderer spriteRenderer;
    private BoxCollider2D boxCollider;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        boxCollider = gameObject.GetComponent<BoxCollider2D>();
        spriteRenderer.sprite = RockSprites[0];
        currentSpriteIndex = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(spriteRenderer.sprite == RockSprites[3])
        {
            boxCollider.enabled = false;   
        }
    }

    public void LightHit()
    {
        currentSpriteIndex++;
        spriteRenderer.sprite = RockSprites[currentSpriteIndex];
    }

    public void HeavyHit()
    {
        spriteRenderer.sprite = RockSprites[3];
    }
}

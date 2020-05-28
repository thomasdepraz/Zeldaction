using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyableRocks : MonoBehaviour
{

    public Sprite[] RockSprites = new Sprite[4];
    public int currentSpriteIndex;
    private SpriteRenderer spriteRenderer;
    private BoxCollider2D boxCollider;
    public GameObject loot;
    private bool looted =false;
    public ParticleSystem hitParticle;
    public PropsAudioManager audioManager;
    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        boxCollider = gameObject.GetComponent<BoxCollider2D>();
        spriteRenderer.sprite = RockSprites[0];
        currentSpriteIndex = 0;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if(spriteRenderer.sprite == RockSprites[3])
        {
            boxCollider.enabled = false;
            spriteRenderer.sortingOrder = -1;
            if (!looted)
            {
                audioManager.PlayClip(audioManager.breakRock, 1, audioManager.rocks);
                float stat = Random.Range(0f, 1f);
                if (stat < 0.5)
                {
                    GameObject.Instantiate(loot, gameObject.transform.position, Quaternion.identity);
                }
                StartCoroutine(RockReset());
                looted = true;
            }
        }
    }
    public void LightHit()
    {
        currentSpriteIndex++;
        spriteRenderer.sprite = RockSprites[currentSpriteIndex];
        hitParticle.Play();
        audioManager.PlayClip(audioManager.hitRock, 1, audioManager.rocks);
    }

    public void HeavyHit()
    {
        spriteRenderer.sprite = RockSprites[3];
        hitParticle.Play();
    }

    private IEnumerator RockReset()
    {
        yield return new WaitForSeconds(30);
        if((player.transform.position - gameObject.transform.position).magnitude > 10)
        {
            currentSpriteIndex = 0;
            spriteRenderer.sortingOrder = 0;
            spriteRenderer.sprite = RockSprites[0];
            boxCollider.enabled = true;
            looted = false;

        }
        else
        {
            StartCoroutine(RockReset());
        }

    }
}

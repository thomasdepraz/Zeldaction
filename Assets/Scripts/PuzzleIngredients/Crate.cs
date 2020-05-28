using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crate : MonoBehaviour
{
    [Header("Sprites")]
    public Sprite lightCrate;
    public Sprite lightCrateRumble;
    public Sprite heavyCrate;
    private SpriteRenderer spr;
    private Rigidbody2D rb;
    
    [Header("Nature")]
    public bool isLightCrate;
    public bool isHeavyCrate;
    public bool isArmor;

    [Header("Elements")]
    public GameObject hitbox;
    private GameObject hook;
    public PropsAudioManager audioManager;
    private bool isMoving;

    [Header("Tweaks")]
    public int crateDamage;






    // Start is called before the first frame update
    void Start()
    {
        hook = GameObject.FindGameObjectWithTag("Hook");
        spr = gameObject.GetComponent<SpriteRenderer>();
        rb = gameObject.GetComponent<Rigidbody2D>();

        if (isLightCrate)
            spr.sprite = lightCrate;
        else if (isHeavyCrate)
            spr.sprite = heavyCrate;
    }

    // Update is called once per frame
    void Update()
    {
        if(rb.velocity.magnitude > 0.5)
        {
            isMoving = true; 
        }
        else
        {
            isMoving = false;
            if(audioManager.soundSource.clip != null)
            {
                if(audioManager.soundSource.clip != audioManager.lightCratebreak && audioManager.soundSource.isPlaying)
                {
                    audioManager.soundSource.Stop();
                }
            }
            
        }

        if(isMoving)
        {
            if(isLightCrate)
            {
                if(!audioManager.soundSource.isPlaying)
                {
                    audioManager.PlayAndLoop(audioManager.lightCrateMove, 1, audioManager.crate);
                }
            }
            else
            {
                if (!audioManager.soundSource.isPlaying)
                {
                    audioManager.PlayAndLoop(audioManager.heavyCrateMove, 1, audioManager.crate);
                }
            }
        }
    }

    void DealDamage(GameObject enemy)
    {
        if (Mathf.Abs(rb.velocity.magnitude) > 7)
        {
            if(enemy.TryGetComponent<EnemyHP>(out EnemyHP enemyHP))
            {
                enemyHP.TakeDamage(crateDamage);
            }

            if(isLightCrate)
            {
                hitbox.SetActive(false);
                gameObject.GetComponent<BoxCollider2D>().enabled = false;
                spr.sprite = lightCrateRumble;
                spr.sortingOrder = -2;
                audioManager.PlayClipNat(audioManager.lightCratebreak, 1, audioManager.crate);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(!collision.collider.gameObject.CompareTag("Player"))
        {
            DealDamage(collision.collider.gameObject);
        }
    }

}

using Cinemachine;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayerHP : MonoBehaviour
{

    [Header ("Player Health")]
    [Range (1,20)]
    public int maxHealth = 20;
    public int currentHealth;
    public int heal = 1;


    public HealthBar healthBar;
    public Image portrait;
    public GameObject gameOverUI;
    public Sprite goodPortrait;
    public Sprite damagePortrait;
    public Material damageMaterial;
    private Material defaultMaterial;
    public PlayerAudioManager playerAudio;
    [HideInInspector] public Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;

        healthBar.SetHealth(maxHealth); // set la barre de vie avec la vie maximale du joueur
        defaultMaterial = GetComponent<SpriteRenderer>().material;

        anim = GetComponent<Animator>();
    }

    public void TakeDamage(int damage)
    {
        if (damage > 0)
        {
            if(currentHealth > 0)
            {
                playerAudio.PlayClip(playerAudio.onHit, 1, playerAudio.life);
            }
        }
        else
        {
            playerAudio.PlayClip(playerAudio.lifeUp, 1, playerAudio.life);
        }

        GetComponent<CinemachineImpulseSource>().GenerateImpulse(Vector3.up);//screenshake

        currentHealth -= damage; // le montant des dommages va être soustrait à la vie actuelle du joueur

        if (currentHealth >= 0)
        {
            healthBar.SetHealth(currentHealth); // met à jour la barre de vie avec la vie actuelle du joueur
        }
        else
        {
            healthBar.SetHealth(0);
        }

        StartCoroutine("portraitSwap");//damaged portrait
        StartCoroutine("DamageFB");//damage FX

        if (currentHealth <= 0)
        {
            playerAudio.PlayClip(playerAudio.death, 1, playerAudio.life);
            anim.SetBool("isDead", true);
        }
    }

    private IEnumerator portraitSwap()
    {
        portrait.sprite = damagePortrait;
        yield return new WaitForSeconds(0.3f);
        portrait.sprite = goodPortrait;
    }

    IEnumerator DamageFB()
    {
        GetComponent<SpriteRenderer>().material = damageMaterial;
        yield return new WaitForSeconds(0.2f);
        GetComponent<SpriteRenderer>().material = defaultMaterial;
    }

    public void DeathUI()
    {
        gameOverUI.SetActive(true);
        GameOverUI.startAnim = true;
        PlayerManager.canAttack = false;
        PlayerManager.canMove = false;
        PlayerManager.useHook = false;
    }
}

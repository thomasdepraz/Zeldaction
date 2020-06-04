﻿using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using System.Collections;

public class GameOverUI : MonoBehaviour
{
    public GameObject gameOverUI;
    private GameObject player;
    public Animator anim;
    private EventSystem eventSystem;
    public GameObject retryButton;
    public bool reset;
    public static bool startAnim = false;
    public UIAudioManager audioManager;
    private bool canStartCoroutine = true;



    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if (reset == true)
        {
            Retry();
            reset = false;
        }
        eventSystem = EventSystem.current;
    }

    private void Update()
    {
        if (startAnim)
        {
            anim.SetBool("GameOverUI", true);
            startAnim = false;
        }
    }

    public void Retry()
    {
        /*player.GetComponent<Rigidbody2D>().constraints &= ~RigidbodyConstraints2D.FreezePositionX;
        player.GetComponent<Rigidbody2D>().constraints &= ~RigidbodyConstraints2D.FreezePositionX;*/
        if (canStartCoroutine)
        {
            StartCoroutine(RetryCoroutine());
        }
    }

    public void MainMenu()
    {
            /*player.GetComponent<Rigidbody2D>().constraints &= ~RigidbodyConstraints2D.FreezePositionX;
            player.GetComponent<Rigidbody2D>().constraints &= ~RigidbodyConstraints2D.FreezePositionX;*/
        if (canStartCoroutine)
        {
            StartCoroutine(QuitCoroutine());
        }
    }

    private IEnumerator QuitCoroutine()
    {
        canStartCoroutine = false;
        eventSystem.enabled = false;
        yield return new WaitUntil(() => !audioManager.soundSource.isPlaying);
        eventSystem.enabled = true;
        canStartCoroutine = true;
        SceneManager.LoadScene("MainMenuScene");
    }

    private IEnumerator RetryCoroutine()
    {
        canStartCoroutine = false;
        eventSystem.enabled = false;
        yield return new WaitUntil(() => !audioManager.soundSource.isPlaying);

        if (SceneManager.GetActiveScene().name != "DungeonScene")
        {
            player.transform.position = PlayerManager.lastCheckpoint.transform.position;
            PlayerManager.canAttack = true;
            PlayerManager.canMove = true;
            PlayerManager.useHook = true;
            PlayerManager.lastCheckpoint.GetComponent<Checkpoint>().ResetFight();
            player.GetComponent<PlayerHP>().currentHealth = 0;
            player.GetComponent<PlayerHP>().TakeDamage(-player.GetComponent<PlayerHP>().maxHealth);//reset la vie du joueur
            Time.timeScale = 1f;
            gameOverUI.SetActive(false);
            player.GetComponent<PlayerHP>().anim.SetBool("isDead", false);
            player.GetComponent<HookThrow>().ResetHook();
        }
        else
        {
            Time.timeScale = 1f;
            PlayerManager.canMove = true;
            SceneManager.LoadScene("DungeonScene");
            player.GetComponent<HookThrow>().ResetHook();
        }
        eventSystem.enabled = true;
        canStartCoroutine = true;
    }
    public void getUIButtons() //AnimEvent
    {
        eventSystem.firstSelectedGameObject = retryButton;
        eventSystem.SetSelectedGameObject(retryButton);
    }
}

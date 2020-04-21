using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour
{
    public GameObject gameOverUI;
    private GameObject player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    public void Retry()
    {
        gameOverUI.SetActive(false);
        player.transform.position = PlayerManager.lastCheckpoint.transform.position;
        player.GetComponent<PlayerHP>().currentHealth = player.GetComponent<PlayerHP>().maxHealth;//reset la vie du joueur
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenuScene");
    }
}

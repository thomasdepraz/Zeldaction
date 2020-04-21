using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour
{
    public GameObject gameOverUI;
    public void Retry()
    {
        gameOverUI.SetActive(false);
        //relancer au dernier checkpoint
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenuScene");
    }
}

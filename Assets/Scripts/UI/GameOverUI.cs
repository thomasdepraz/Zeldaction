using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour
{
    public GameObject gameOverUI;
    private GameObject player;
    public bool reset;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if(reset == true)
        {
            Retry();
            reset = false;
        }
    }

    public void Retry()
    {
        if(SceneManager.GetActiveScene().name != "DungeonScene")
        {
            player.transform.position = PlayerManager.lastCheckpoint.transform.position;
            PlayerManager.lastCheckpoint.GetComponent<Checkpoint>().ResetFight();
            player.GetComponent<PlayerHP>().currentHealth = 0;
            player.GetComponent<PlayerHP>().TakeDamage(-player.GetComponent<PlayerHP>().maxHealth);//reset la vie du joueur
            Time.timeScale = 1f;
            gameOverUI.SetActive(false);
        }
        else
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene("DungeonScene");
        }
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenuScene");
    }
}

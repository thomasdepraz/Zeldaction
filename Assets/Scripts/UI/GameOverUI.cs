using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class GameOverUI : MonoBehaviour
{
    public GameObject gameOverUI;
    private GameObject player;
    public Animator anim;
    private EventSystem eventSystem;
    public GameObject retryButton;
    public bool reset;
    public static bool startAnim = false;
     


    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if(reset == true)
        {
            Retry();
            reset = false;
        }
        eventSystem = EventSystem.current;
    }

    private void Update()
    {
        if(startAnim)
        {
            anim.SetBool("GameOverUI", true);
            startAnim = false;
        }
    }

    public void Retry()
    {
        if(SceneManager.GetActiveScene().name != "DungeonScene")
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

    public void getUIButtons() //AnimEvent
    {
        eventSystem.firstSelectedGameObject = retryButton;
        eventSystem.SetSelectedGameObject(retryButton);
    }

}

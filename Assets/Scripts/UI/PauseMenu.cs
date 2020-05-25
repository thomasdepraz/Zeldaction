using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using System.Collections;
using System.Diagnostics;

public class PauseMenu : MonoBehaviour
{

    public GameObject pauseMenu;
    public GameObject gameOverPanel;
    private bool isPaused;
    private EventSystem eventSystem;
    public GameObject pauseButton;
    public UIAudioManager audioManager;
    private bool canStartCoroutine = true;
    private GameObject player;
    public GameObject retryButton;
    // Start is called before the first frame update
    void Start()
    {
        eventSystem = EventSystem.current;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if(PlayerManager.lastCheckpoint != null || SceneManager.GetActiveScene().name == "DungeonScene")
        {
            retryButton.SetActive(true);
        }
        if(!gameOverPanel.activeSelf)
        {
            if (Input.GetButtonDown("StartButton"))
            {
                if (!isPaused)
                {
                    Pause();

                }
                else
                {
                    Resume();

                }
            }        
        }
    }

    public void Pause()
    {
        audioManager.PlayClip(audioManager.soundSource, audioManager.openMenu, 1, audioManager.menu);
        eventSystem.firstSelectedGameObject = pauseButton;
        eventSystem.SetSelectedGameObject(pauseButton);
        pauseMenu.SetActive(true);
        Time.timeScale = 0;
        isPaused = true;
    }

    public void Resume()
    {
        if(canStartCoroutine)
        {
            StartCoroutine(ResumeCoroutine());
        }
    }

    public void Quit()
    {
        if(canStartCoroutine)
        {
            StartCoroutine(QuitCoroutine());
        }
    }

    public void Retry()
    {
        if (canStartCoroutine)
        {
            StartCoroutine(RetryCoroutine());
        }
    }

    private IEnumerator ResumeCoroutine()
    {
        canStartCoroutine = false;
        eventSystem.enabled = false;
        yield return new WaitUntil(() => !audioManager.soundSource.isPlaying);
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
        isPaused = false;
        audioManager.PlayClip(audioManager.soundSource, audioManager.closeMenu, 1, audioManager.menu);
        eventSystem.enabled = true;
        canStartCoroutine = true;
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
            PlayerManager.lastCheckpoint.GetComponent<Checkpoint>().ResetFight();
            Time.timeScale = 1f;
            isPaused = false;
            pauseMenu.SetActive(false);
            player.GetComponent<PlayerHP>().anim.SetBool("isDead", false);
        }
        else
        {
            isPaused = false;
            Time.timeScale = 1f;
            SceneManager.LoadScene("DungeonScene");
        }
        eventSystem.enabled = true;
        canStartCoroutine = true;
    }

}

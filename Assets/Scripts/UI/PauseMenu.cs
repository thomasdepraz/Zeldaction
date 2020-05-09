using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class PauseMenu : MonoBehaviour
{

    public GameObject pauseMenu;
    public GameObject gameOverPanel;
    private bool isPaused;
    public EventSystem eventSystem;
    public GameObject pauseButton;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
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
        eventSystem.firstSelectedGameObject = pauseButton;
        eventSystem.SetSelectedGameObject(pauseButton);
        pauseMenu.SetActive(true);
        Time.timeScale = 0;
        isPaused = true;
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
        isPaused = false;
    }

    public void Options()
    {
        //WIP
    }

    public void Quit()
    {
        SceneManager.LoadScene("MainMenuScene");
    }
}

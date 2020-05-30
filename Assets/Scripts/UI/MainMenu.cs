using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public UIAudioManager audioManager;
    public AudioSource mainThemeSource;
    public float maxVolume;

    private void Start()
    {
        StartCoroutine(FadeSound());
    }
    public void Play()
    {
        StartCoroutine(PlayCoroutine());
        
    }

    public void Options()
    {

    }

    public void Quit()
    {
        StartCoroutine(QuitCoroutine());
    }

    private IEnumerator PlayCoroutine()
    {
        yield return new WaitUntil(() => !audioManager.soundSource.isPlaying);
        Time.timeScale = 1f;
        SceneManager.LoadScene("CinematicScene");
    }

    private IEnumerator QuitCoroutine()
    {
        yield return new WaitUntil(() => !audioManager.soundSource.isPlaying);
        Time.timeScale = 1f;
        Application.Quit();
    }

    private IEnumerator FadeSound()
    {
        while(mainThemeSource.volume <=  maxVolume)
        {
            yield return new WaitForSeconds(0.1f);
            mainThemeSource.volume += 0.01f;
        }
    }

    public void hookScene()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainScene_Hook");
    }
    public void DestroyedVillageScene()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainScene_DestroyedVillage");
    }
    public void Boss()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("DungeonScene");
    }

}

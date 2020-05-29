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
        SceneManager.LoadScene("CinematicScene");
    }

    private IEnumerator QuitCoroutine()
    {
        yield return new WaitUntil(() => !audioManager.soundSource.isPlaying);
        Application.Quit();
    }

    private IEnumerator FadeSound()
    {
        while(mainThemeSource.volume <=  maxVolume)
        {
            Debug.Log("heho");
            yield return new WaitForSeconds(0.3f);
            mainThemeSource.volume += 0.001f;
        }
    }

}

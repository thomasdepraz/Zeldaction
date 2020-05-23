using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public UIAudioManager audioManager;
    
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
        SceneManager.LoadScene("MainScene");
    }

    private IEnumerator QuitCoroutine()
    {
        yield return new WaitUntil(() => !audioManager.soundSource.isPlaying);
        Application.Quit();
    }

}

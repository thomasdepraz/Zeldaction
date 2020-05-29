using UnityEngine;
using UnityEngine.SceneManagement;

public class goToMainMenu : MonoBehaviour
{
void goBack()
    {
        SceneManager.LoadScene("MainMenuScene");
    }
}

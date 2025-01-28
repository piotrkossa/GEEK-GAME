using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] Scene MainScene;

    public void PlayGame()
    {
        SceneManager.LoadScene("MainScene", LoadSceneMode.Single);
    }

    public void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
}

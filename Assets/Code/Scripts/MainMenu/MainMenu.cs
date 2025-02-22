using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] Scene MainScene;
    [SerializeField] private SceneTransition _sceneTransition;

    private bool isTrans = false;

    public async void PlayGame()
    {
        if (isTrans) return;
        isTrans = true;
        await _sceneTransition.EndTransition();
        SceneManager.LoadScene("MainScene", LoadSceneMode.Single);

        isTrans = false;
    }

    public void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
}

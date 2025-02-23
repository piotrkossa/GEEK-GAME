using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] Scene MainScene;
    [SerializeField] private SceneTransition _sceneTransition;
    AudioManager audioManager;
    private bool isTrans = false;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    public async void PlayGame()
    {
        if (isTrans) return;
        isTrans = true;
        audioManager.PlaySFX(audioManager.choice);
        await _sceneTransition.EndTransition();
        SceneManager.LoadScene("Cutscene", LoadSceneMode.Single);

        isTrans = false;
    }

    public void QuitGame()
    {
        Debug.Log("Quit");
        audioManager.PlaySFX(audioManager.choice);
        Application.Quit();
    }
}

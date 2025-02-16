using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;

    public void Play()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
        InputManager.Instance.UnlockAllInput();
    }

    public void Settings()
    {

    }

    public void Help()
    {

    }

    public void Menu()
    {
        SceneManager.LoadScene("MainMenu");
        Time.timeScale = 1;
    }
}

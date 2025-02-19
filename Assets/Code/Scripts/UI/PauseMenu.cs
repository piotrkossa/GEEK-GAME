using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GlobalVariables _globalVariables;

    private void Start()
    {
        this.gameObject.SetActive(false);
        InputManager.Instance.OnEscUnlocked.AddListener(CheckForEsc);
    }

    public void Play()
    {
        this.gameObject.SetActive(false);
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

    private void PauseClicked()
    {
        InputManager.Instance.BlockAllInput();
        this.gameObject.SetActive(true);
        Time.timeScale = 0;
    }

    private void CheckForEsc() {
        if (this.gameObject.activeSelf == true) {
            this.gameObject.SetActive(false);
            Time.timeScale = 1;
            InputManager.Instance.UnlockAllInput();
            return;
        }

        if (_globalVariables.CurrentLevelView == 0 && InputManager.Instance.isInputBlocked == false)
            PauseClicked();
    }
}

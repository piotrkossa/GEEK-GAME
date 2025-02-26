using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GlobalVariables _globalVariables;
    [SerializeField] GameObject settingsScreen;
    [SerializeField] GameObject buttonsScreen;
    [SerializeField] private SceneTransition _sceneTransition;

    AudioManager audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }


    private void Start()
    {
        this.gameObject.SetActive(false);
        InputManager.Instance.OnEscUnlocked.AddListener(CheckForEsc);
    }

    public void Play()
    {
        audioManager.PlaySFX(audioManager.choice);
        this.gameObject.SetActive(false);
        Time.timeScale = 1;
        InputManager.Instance.UnlockAllInput();
    }

    public void Settings()
    {
        audioManager.PlaySFX(audioManager.choice);
        buttonsScreen.SetActive(false);
        settingsScreen.SetActive(true);
    }

    public void Help()
    {
        audioManager.PlaySFX(audioManager.choice);
    }

    public async void Menu()
    {
        audioManager.PlaySFX(audioManager.choice);
        
        await _sceneTransition.EndTransition();
        SceneManager.LoadScene("MainMenu");;
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

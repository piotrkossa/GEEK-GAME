using UnityEngine;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
    public GameObject[] screens;
    private int currentScreenIndex = 0;
    [SerializeField] GameObject buttonsScreen;
    AudioManager audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    private void Start()
    {
        ShowScreen(currentScreenIndex);
    }

    private void ShowScreen(int index)
    {
        foreach (GameObject screen in screens)
        {
            screen.SetActive(false);
        }

        if (index >= 0 && index < screens.Length)
        {
            screens[index].SetActive(true);
        }
    }

    public void NextScreen()
    {
        if (currentScreenIndex < screens.Length - 1)
        {
            currentScreenIndex++;
            audioManager.PlaySFX(audioManager.choice);
            ShowScreen(currentScreenIndex);
        }
    }

    public void PreviousScreen()
    {
        if (currentScreenIndex > 0)
        {

            currentScreenIndex--;
            audioManager.PlaySFX(audioManager.choice);
            ShowScreen(currentScreenIndex);
        }
    }

    public void Exit()
    {
        audioManager.PlaySFX(audioManager.choice);
        gameObject.SetActive(false);
        InputManager.Instance.UnlockAllInput();
        Time.timeScale = 1;
    }
}

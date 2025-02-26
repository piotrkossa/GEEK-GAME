using UnityEngine;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
    public GameObject[] screens;
    private int currentScreenIndex = 0;
    [SerializeField] GameObject buttonsScreen;

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
            ShowScreen(currentScreenIndex);
        }
    }

    public void PreviousScreen()
    {
        if (currentScreenIndex > 0)
        {
            currentScreenIndex--;
            ShowScreen(currentScreenIndex);
        }
    }

    public void Exit()
    {
        gameObject.SetActive(false);
        InputManager.Instance.UnlockAllInput();
        Time.timeScale = 1;
    }
}

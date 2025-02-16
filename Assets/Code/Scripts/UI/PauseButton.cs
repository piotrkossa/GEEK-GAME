using UnityEngine;

public class PauseButton : MonoBehaviour
{

    [SerializeField] private GameObject PauseMenu;
    [SerializeField] private GlobalVariables _globalVariables;

    private void Start()
    {
        InputManager.Instance.OnGameObjectClicked.AddListener(CheckIfPauseClicked);
        InputManager.Instance.OnEscUnlocked.AddListener(CheckForEsc);
    }

    private void CheckIfPauseClicked(Transform objTransform)
    {
        if (objTransform.name == this.name)
        {
            PauseClicked();
        }
    }

    private void PauseClicked()
    {
        InputManager.Instance.BlockAllInput();
        PauseMenu.SetActive(true);
        Time.timeScale = 0;
    }

    private void CheckForEsc() {
        if (PauseMenu.activeSelf == true) {
            PauseMenu.SetActive(false);
            Time.timeScale = 1;
            InputManager.Instance.UnlockAllInput();
            return;
        }

        if (_globalVariables.CurrentLevelView == 0 && InputManager.Instance.isInputBlocked == false)
            PauseClicked();
    }
}

using UnityEngine;

public class PauseButton : MonoBehaviour
{

    [SerializeField] GameObject PauseMenu;
    private void Start()
    {
        InputManager.Instance.OnGameObjectClicked.AddListener(CheckIfPauseClicked);
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
        PauseMenu.SetActive(true);
        Time.timeScale = 0;
    }
}

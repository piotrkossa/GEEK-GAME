using UnityEngine;

public class MainUI : MonoBehaviour
{
    [SerializeField] GlobalVariables _globalVariables;

    private void Start() {
        _globalVariables.onViewChanged.AddListener(ChangeVisibleState);
        this.gameObject.SetActive(false);
    }

    public GlobalVariables GetGlobalVariables() {
        return _globalVariables;
    }

    private void ChangeVisibleState() {
        if (_globalVariables.CurrentLevelView != 0)
            this.gameObject.SetActive(true);
        else
            this.gameObject.SetActive(false);
    }
}

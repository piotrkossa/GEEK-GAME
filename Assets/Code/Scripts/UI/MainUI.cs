using UnityEngine;

public class MainUI : MonoBehaviour
{
    [SerializeField] GlobalVariables _globalVariables;

    private void Awake() {
        _globalVariables.onViewChanged.AddListener(ChangeVisibleState);
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

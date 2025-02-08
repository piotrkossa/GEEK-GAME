using UnityEngine;

public class MainUI : MonoBehaviour
{
    [SerializeField] GlobalVariables _globalVariables;
    [SerializeField] private TransitionManager _transitionManager;

    private CanvasGroup _thisCanvasGroup;

    private void Start() {
        _globalVariables.onViewChanged.AddListener(ChangeVisibleState);
        _thisCanvasGroup = this.GetComponent<CanvasGroup>();
        _thisCanvasGroup.alpha = 0f;
        this.gameObject.SetActive(false);
    }

    public GlobalVariables GetGlobalVariables() {
        return _globalVariables;
    }

    private bool isChanging = false;

    private async void ChangeVisibleState() {
        if (isChanging)
            return;
        
        isChanging = true;

        if (_globalVariables.CurrentLevelView != 0) {
            this.gameObject.SetActive(true);
            await _transitionManager.CanvasGroupAlphaTransition(_thisCanvasGroup, 0f, 1f, 1f, 0.55f, 1f);
        }
        else {
            await _transitionManager.CanvasGroupAlphaTransition(_thisCanvasGroup, 1f, 0f, 1f, 1f, 0.55f);
            this.gameObject.SetActive(false);
        }
        isChanging = false;
    }
}

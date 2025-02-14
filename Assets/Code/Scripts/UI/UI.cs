using UnityEngine;
using UnityEngine.EventSystems;

public class UI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] GlobalVariables _globalVariables;
    [SerializeField] private TransitionManager _transitionManager;

    private CanvasGroup _thisCanvasGroup;


    private void Start() {
        _thisCanvasGroup = this.GetComponent<CanvasGroup>();
        SwitchAvailability(false);
    }

    public GlobalVariables GetGlobalVariables() {
        return _globalVariables;
    }

    public void OnPointerEnter(PointerEventData data) {
        SwitchAvailability(true);
    }

    public void OnPointerExit(PointerEventData data)
    {
        SwitchAvailability(false);
    }

    private void SwitchAvailability(bool state) {
        if (state) {
            _thisCanvasGroup.alpha = 1f;
            _thisCanvasGroup.interactable = true;
        }
        else {
            _thisCanvasGroup.alpha = 0f;
            _thisCanvasGroup.interactable = false;
        }
    }
}

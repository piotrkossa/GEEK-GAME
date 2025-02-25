using UnityEngine;
using UnityEngine.EventSystems;

public class SwitchVisibility : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private GlobalVariables _globalVariables;
    private static Vector3 originPosition;

    private static Vector3 hidenPosition;

    private RectTransform thisRectTransform;

    [Header("Settings")]
    [SerializeField] private float transitionTime;

    private bool isPointerIn = false;

    private void Start() {
        thisRectTransform = this.GetComponent<RectTransform>();

        originPosition = Vector3.zero;
        hidenPosition = originPosition - new Vector3(0, 123, 0);

        _globalVariables.onViewChanged.AddListener(() => {if (_globalVariables.CurrentLevelView == 0) HideThis();});

        thisRectTransform.anchoredPosition = hidenPosition;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        isPointerIn = true;
        ShowThis();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isPointerIn = false;
        HideThis();
    }

    private bool isWorking = false;

    private async void HideThis() {
        if (isWorking) return;
        isWorking = true;
        await TransitionManager.Instance.AnchoredPositionTransition(thisRectTransform, originPosition, hidenPosition, transitionTime, 0f, 1f);
        isWorking = false;
    }


    private async void ShowThis() {
        if (isWorking) return;
        isWorking = true;
        await TransitionManager.Instance.AnchoredPositionTransition(thisRectTransform, hidenPosition, originPosition, transitionTime, 0f, 1f);
        isWorking = false;
        if (!isPointerIn) HideThis();
    }
}

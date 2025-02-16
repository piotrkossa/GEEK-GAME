using UnityEngine;
using UnityEngine.EventSystems;

public class SwitchVisibility : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private static Vector3 originPosition;

    private static Vector3 hidenPosition;

    [Header("Settings")]
    [SerializeField] private float transitionTime;

    private bool isPointerIn = false;

    private void Start() {
        this.transform.GetComponent<RectTransform>().anchoredPosition = Vector3.zero;
        originPosition = this.transform.position;
        hidenPosition = originPosition - new Vector3(0, 123, 0);

        this.transform.position = hidenPosition;
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
        await TransitionManager.Instance.VectorTransition(this.transform, this.transform.position, hidenPosition, transitionTime, 0f, 1f);
        isWorking = false;
    }


    private async void ShowThis() {
        if (isWorking) return;
        isWorking = true;
        await TransitionManager.Instance.VectorTransition(this.transform, this.transform.position, originPosition, transitionTime, 0f, 1f);
        isWorking = false;
        if (!isPointerIn) HideThis();
    }
}

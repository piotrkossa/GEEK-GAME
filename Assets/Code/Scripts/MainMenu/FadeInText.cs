using UnityEngine;
using TMPro;
using System.Collections;

public class FadeInTextMeshPro : MonoBehaviour
{
    private RectTransform _title;
    private Vector3 originPos;

    void Start()
    {
        _title = this.GetComponent<RectTransform>();
        originPos = _title.anchoredPosition;
        _title.anchoredPosition -= new Vector2(900, 0);
        
        FadeIn();
    }

    private async void FadeIn()
    {
        await Awaitable.WaitForSecondsAsync(1f);
        await TransitionManager.Instance.AnchoredPositionTransition(_title, _title.anchoredPosition, originPos, 0.8f, 1f, 1f);
    }
}

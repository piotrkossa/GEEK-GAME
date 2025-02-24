using UnityEngine;
using TMPro;
using System.Collections;

public class FadeInTextMeshPro : MonoBehaviour
{
    private Transform _title;
    private Vector3 originPos;

    void Start()
    {
        _title = this.transform;
        originPos = _title.position;
        _title.position -= new Vector3(900, 0, 0);
        
        FadeIn();
    }

    private async void FadeIn()
    {
        await Awaitable.WaitForSecondsAsync(1.5f);
        await TransitionManager.Instance.VectorTransition(_title, _title.position, originPos, 0.8f, 1f, 1f);
    }
}

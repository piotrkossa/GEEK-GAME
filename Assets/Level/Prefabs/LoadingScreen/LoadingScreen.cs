using System.Collections;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

public class LoadingScreen : MonoBehaviour
{
    [SerializeField] private Transform _logo;
    [SerializeField] private Slider _slider;

    private void Start() {
        _slider.value = 1f;
        _logo.localScale = Vector3.zero;
        LogoAnimation();
    }

    private async void LogoAnimation() {
        await Awaitable.WaitForSecondsAsync(1f);

        await TransitionManager.Instance.ScaleTransition(_logo, Vector3.zero, Vector3.one * 1.4f, 0.3f, 0f, 1f);

        await TransitionManager.Instance.ScaleTransition(_logo, _logo.localScale, Vector3.one, 0.3f, 0f, 1f);

        await TransitionManager.Instance.SliderValueTransition(_slider, 1f, 0f, 1f, 0.37f, 0.63f);
    }
}

using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingScreen : MonoBehaviour
{
    [SerializeField] private Transform _logo;
    [SerializeField] private Slider _slider;

    private void Start() {
        _slider.value = 1f;
        _logo.localScale = Vector3.zero;
        LogoAnimation();
        PlayerPrefs.SetInt("isAfterLoadingScreen", 1);
    }

    private async void LogoAnimation() {
        await Awaitable.WaitForSecondsAsync(1f);

        await TransitionManager.Instance.ScaleTransition(_logo, Vector3.zero, Vector3.one * 1.4f, 0.3f, 0f, 1f);
        await TransitionManager.Instance.ScaleTransition(_logo, _logo.localScale, Vector3.one, 0.3f, 0f, 1f);

        await Awaitable.WaitForSecondsAsync(0.5f);

        await TransitionManager.Instance.SliderValueTransition(_slider, 1f, 0f, 1f, 0.37f, 0.63f);

        await Awaitable.WaitForSecondsAsync(2f);

        Task[] tasks = new Task [2];

        tasks[0] = TransitionManager.Instance.ScaleTransition(_logo, _logo.localScale, Vector3.one * 1.4f, 0.3f, 0f, 1f);
        tasks[1] = TransitionManager.Instance.ScaleTransition(_slider.transform, _logo.localScale, Vector3.one * 1.4f, 0.3f, 0f, 1f);

        await Task.WhenAll(tasks);

        tasks[0] = TransitionManager.Instance.ScaleTransition(_logo, Vector3.one * 1.4f, Vector3.zero, 0.3f, 1f, 1f);
        tasks[1] = TransitionManager.Instance.ScaleTransition(_slider.transform, Vector3.one * 1.4f, Vector3.zero, 0.3f, 1f, 1f);

        await Task.WhenAll(tasks);

        await Awaitable.WaitForSecondsAsync(0.2f);

        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }
}

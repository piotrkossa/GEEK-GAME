using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneTransition : MonoBehaviour
{
    [SerializeField] private RectTransform _topRect;
    [SerializeField] private RectTransform _botRect;
    [SerializeField] private Image _icon;

    private Color iconStartingColor = Color.white;
    private Color iconEndingColor = new Color(1f, 1f, 1f, 0f);

    private Vector3 startingTopRectPos;
    private Vector3 startingBotRectPos;

    private Vector3 topRectGoal;
    private Vector3 botRectGoal;

    AudioManager audioManager;

    private bool startWithoutIcon = false;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    private void Start() {
        startingTopRectPos = _topRect.anchoredPosition;
        startingBotRectPos = _botRect.anchoredPosition;

        topRectGoal = startingTopRectPos + new Vector3(0, 540, 0);
        botRectGoal = startingBotRectPos + new Vector3(0, -540, 0);

        if (SceneManager.GetActiveScene().name == "MainMenu" && PlayerPrefs.GetInt("isAfterLoadingScreen") == 1) {
            _icon.color = iconEndingColor;
            startWithoutIcon = true;
            PlayerPrefs.SetInt("isAfterLoadingScreen", 0);
        }

        StartTransition();
    }


    private async void StartTransition() {
        this.gameObject.SetActive(true);

        if (!startWithoutIcon) {
            audioManager.PlaySFX(audioManager.kittenMeow);
            await Task.Delay(1000);
            await TransitionManager.Instance.ImageColorTransition(_icon, iconStartingColor, iconEndingColor, 1.5f, 1f, 1f);
            await Task.Delay(700);
        }

        Task[] tasks = new Task [2];

        tasks[0] = TransitionManager.Instance.AnchoredPositionTransition(_topRect, startingTopRectPos, topRectGoal, 1f, 0f, 0f);
        tasks[1] = TransitionManager.Instance.AnchoredPositionTransition(_botRect, startingBotRectPos, botRectGoal, 1f, 0f, 0f);

        await Task.WhenAll(tasks);
        this.gameObject.SetActive(false);
    }

    public async Task EndTransition() {
        this.gameObject.SetActive(true);
        Task[] tasks = new Task [2];

        tasks[0] = TransitionManager.Instance.AnchoredPositionTransition(_topRect, topRectGoal, startingTopRectPos, 1f, 0f, 0f);
        tasks[1] = TransitionManager.Instance.AnchoredPositionTransition(_botRect, botRectGoal, startingBotRectPos, 1f, 0f, 0f);

        await Task.WhenAll(tasks);

        await Task.Delay(700);

        await TransitionManager.Instance.ImageColorTransition(_icon, iconEndingColor, iconStartingColor, 1.5f, 0f, 0f);
    }
}

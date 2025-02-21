using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class SceneTransition : MonoBehaviour
{
    [SerializeField] private Transform _topRect;
    [SerializeField] private Transform _botRect;
    [SerializeField] private Image _icon;

    private Color iconStartingColor = Color.white;
    private Color iconEndingColor = new Color(1f, 1f, 1f, 0f);

    private Vector3 startingTopRectPos;
    private Vector3 startingBotRectPos;

    private Vector3 topRectGoal;
    private Vector3 botRectGoal;

    private void Start() {
        startingTopRectPos = _topRect.position;
        startingBotRectPos = _botRect.position;

        topRectGoal = startingTopRectPos + new Vector3(0, 540, 0);
        botRectGoal = startingBotRectPos + new Vector3(0, -540, 0);
        showcase();
    }

    private async void showcase() {
        await StartTransition();
        await EndTransition();
    }


    private async Task StartTransition() {
        await Awaitable.WaitForSecondsAsync(2f);
        await TransitionManager.Instance.ImageColorTransition(_icon, iconStartingColor, iconEndingColor, 1.5f, 1f, 1f);

        await Awaitable.WaitForSecondsAsync(0.7f);

        Task[] tasks = new Task [2];

        tasks[0] = TransitionManager.Instance.VectorTransition(_topRect, startingTopRectPos, topRectGoal, 1f, 0f, 0f);
        tasks[1] = TransitionManager.Instance.VectorTransition(_botRect, startingBotRectPos, botRectGoal, 1f, 0f, 0f);

        await Task.WhenAll(tasks);
    }

    public async Task EndTransition() {
        Task[] tasks = new Task [2];

        tasks[0] = TransitionManager.Instance.VectorTransition(_topRect, topRectGoal, startingTopRectPos, 1f, 0f, 0f);
        tasks[1] = TransitionManager.Instance.VectorTransition(_botRect, botRectGoal, startingBotRectPos, 1f, 0f, 0f);

        await Task.WhenAll(tasks);

        await Awaitable.WaitForSecondsAsync(0.7f);

        await TransitionManager.Instance.ImageColorTransition(_icon, iconEndingColor, iconStartingColor, 1.5f, 0f, 0f);
    }
}

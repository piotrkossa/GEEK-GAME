using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Cutscene : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] SceneTransition _sceneTransition;
    [SerializeField] private Sprite[] _sceneSprites = new Sprite [13];
    [SerializeField] private Image _image;
    private int slideCounter = 0;
    AudioManager audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
            ChangeSlide();
    }

    private bool isTrans = false;

    private async void ChangeSlide() {
        if (isTrans) return;
        isTrans = true;
        slideCounter++;

        if (slideCounter >= _sceneSprites.Count()) {
            GoToMainScene();
            return;
        }

        await TransitionManager.Instance.ImageColorTransition(_image, Color.white, Color.black, 0.4f, 0f, 0f);
        _image.sprite = _sceneSprites[slideCounter];
        await TransitionManager.Instance.ImageColorTransition(_image, Color.black, Color.white, 0.4f, 1f, 1f);
        isTrans = false;
    }

    private async void GoToMainScene() {
        await _sceneTransition.EndTransition();
        SceneManager.LoadScene("MainScene");
    }

    public void SkipClicked()
    {
        GoToMainScene();
        audioManager.PlaySFX(audioManager.choice);
    }
}

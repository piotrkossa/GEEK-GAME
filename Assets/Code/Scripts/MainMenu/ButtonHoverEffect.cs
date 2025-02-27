using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIButtonEffect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField] private TransitionManager _transitionManager;

    private RectTransform thisButton;
    public GameObject fade;
    public float moveDistance = 25f;

    private Vector3 originalPosition;
    private Vector3 goalPosition;
    AudioManager audioManager;

    private bool started = false;

    private void Start()
    {
        thisButton = this.GetComponent<RectTransform>();

        originalPosition = thisButton.anchoredPosition;

        goalPosition = originalPosition + new Vector3(moveDistance, 0, 0);
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        started = true;
    }

    private void OnEnable()
    {
        if (!started) return;
        thisButton.anchoredPosition = originalPosition;
        fade.SetActive(false);
    }


    public async void OnPointerEnter(PointerEventData eventData)
    {
        audioManager.PlaySFX(audioManager.backlight);
        _transitionManager.StopTransition(this.gameObject);
        fade.SetActive(true);
        await _transitionManager.AnchoredPositionTransition(thisButton, originalPosition, goalPosition, 0.3f, 0.55f, 1f);
    }

    public async void OnPointerExit(PointerEventData eventData)
    {
        _transitionManager.StopTransition(this.gameObject);
        fade.SetActive(false);
        await _transitionManager.AnchoredPositionTransition(thisButton, goalPosition, originalPosition, 0.3f, 0.55f, 1f);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        audioManager.PlaySFX(audioManager.choice);
        // Przywrócenie oryginalnej pozycji, rozmiaru i sprite'a po kliknięciu
        thisButton.anchoredPosition = originalPosition;
        fade.SetActive(false); // Przywrócenie podstawowego kostiumu

        // Opcjonalnie, możesz dodać inne akcje po kliknięciu, jak przejście do nowej sceny itp.
    }
}

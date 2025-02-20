using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIButtonEffect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField] private TransitionManager _transitionManager;

    public Transform thisButton;
    public Sprite normalSprite;
    public GameObject fade;
    public float moveDistance = 10f;

    private Vector3 originalPosition;
    AudioManager audioManager;
    void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        thisButton = this.transform;
        originalPosition = thisButton.position;
    }


    public async void OnPointerEnter(PointerEventData eventData)
    {
        _transitionManager.StopTransition(this.gameObject);
        fade.SetActive(true);
        await _transitionManager.VectorTransition(this.transform, originalPosition, originalPosition + new Vector3(moveDistance, 0, 0), 0.3f, 0.55f, 1f);
    }

    public async void OnPointerExit(PointerEventData eventData)
    {
        _transitionManager.StopTransition(this.gameObject);
        fade.SetActive(false);
        await _transitionManager.VectorTransition(this.transform, originalPosition + new Vector3(moveDistance, 0, 0), originalPosition, 0.3f, 0.55f, 1f);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        audioManager.PlaySFX(audioManager.choice);
        // Przywrócenie oryginalnej pozycji, rozmiaru i sprite'a po kliknięciu
        thisButton.position = originalPosition;
        fade.SetActive(false); // Przywrócenie podstawowego kostiumu

        // Opcjonalnie, możesz dodać inne akcje po kliknięciu, jak przejście do nowej sceny itp.
    }
}

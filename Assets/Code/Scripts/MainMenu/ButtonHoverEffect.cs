using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIButtonEffect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField] private TransitionManager _transitionManager;

    public Image buttonImage;
    public Sprite normalSprite;
    public Sprite hoverSprite;
    public float moveDistance = 10f;

    private Vector3 originalPosition;

    void Start()
    {
        if (buttonImage == null)
            buttonImage = GetComponent<Image>(); // Pobranie Image, jeśli nie przypisano

        originalPosition = buttonImage.transform.position;
    }


    public async void OnPointerEnter(PointerEventData eventData)
    {
        _transitionManager.StopTransition(this.gameObject);
        buttonImage.sprite = hoverSprite;
        await _transitionManager.VectorTransition(this.transform, originalPosition, originalPosition + new Vector3(moveDistance, 0, 0), 0.3f, 0.55f, 1f);
    }

    public async void OnPointerExit(PointerEventData eventData)
    {
        _transitionManager.StopTransition(this.gameObject);
        buttonImage.sprite = normalSprite;
        await _transitionManager.VectorTransition(this.transform, originalPosition + new Vector3(moveDistance, 0, 0), originalPosition, 0.3f, 0.55f, 1f);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        // Przywrócenie oryginalnej pozycji, rozmiaru i sprite'a po kliknięciu
        buttonImage.transform.position = originalPosition;
        buttonImage.sprite = normalSprite; // Przywrócenie podstawowego kostiumu

        // Opcjonalnie, możesz dodać inne akcje po kliknięciu, jak przejście do nowej sceny itp.
    }
}

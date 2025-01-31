using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIButtonEffect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public Image buttonImage; // Obrazek przycisku (np. ikona)
    public Sprite normalSprite; // Standardowy wygląd
    public Sprite hoverSprite; // Wygląd po najechaniu
    public float moveDistance = 10f; // Przesunięcie przycisku w prawo
    public float scaleFactor = 1.05f; // Współczynnik powiększenia (np. 1.05 oznacza 5% większy)

    private Vector2 originalPosition; // Początkowa pozycja
    private Vector2 originalSize; // Oryginalny rozmiar

    void Start()
    {
        if (buttonImage == null)
            buttonImage = GetComponent<Image>(); // Pobranie Image, jeśli nie przypisano

        originalPosition = buttonImage.rectTransform.anchoredPosition;
        originalSize = buttonImage.rectTransform.sizeDelta; // Zapamiętaj oryginalny rozmiar
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        buttonImage.sprite = hoverSprite; // Zmiana na nowy wygląd
        buttonImage.rectTransform.anchoredPosition += new Vector2(moveDistance, 0); // Przesunięcie w prawo

        // Zwiększenie rozmiaru o "scaleFactor"
        buttonImage.rectTransform.sizeDelta = originalSize * scaleFactor; // Powiększenie o 5%
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        buttonImage.sprite = normalSprite; // Powrót do standardowego wyglądu
        buttonImage.rectTransform.anchoredPosition = originalPosition; // Powrót na pierwotne miejsce

        // Przywrócenie oryginalnego rozmiaru
        buttonImage.rectTransform.sizeDelta = originalSize;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        // Przywrócenie oryginalnej pozycji, rozmiaru i sprite'a po kliknięciu
        buttonImage.rectTransform.anchoredPosition = originalPosition;
        buttonImage.rectTransform.sizeDelta = originalSize;
        buttonImage.sprite = normalSprite; // Przywrócenie podstawowego kostiumu

        // Opcjonalnie, możesz dodać inne akcje po kliknięciu, jak przejście do nowej sceny itp.
    }
}

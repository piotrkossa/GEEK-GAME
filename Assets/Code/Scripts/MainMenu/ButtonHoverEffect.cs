using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems; // Potrzebne do obsługi zdarzeń myszy

public class ButtonHoverEffect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Sprite defaultSprite; // Obrazek domyślny
    public Sprite hoverSprite;   // Obrazek po najechaniu myszką

    private Image buttonImage;   // Referencja do obrazu przycisku
    public Vector3 hoverScale = new Vector3(1.2f, 1.2f, 1.2f); // Skala po najechaniu
    public Vector3 defaultScale = Vector3.one; // Domyślna skala (1,1,1)

    void Start()
    {
        // Pobierz komponent Image z przycisku
        buttonImage = GetComponent<Image>();

        // Ustaw domyślny obrazek
        buttonImage.sprite = defaultSprite;
        // Ustaw domyślną skalę
        transform.localScale = defaultScale;
    }

    // Zdarzenie po najechaniu myszką
    public void OnPointerEnter(PointerEventData eventData)
    {
        buttonImage.sprite = hoverSprite;
        transform.localScale = hoverScale; // Powiększ przycisk

    }

    // Zdarzenie po opuszczeniu myszką
    public void OnPointerExit(PointerEventData eventData)
    {
        buttonImage.sprite = defaultSprite;
        transform.localScale = defaultScale; // Przywróć domyślną skalę
    }
}

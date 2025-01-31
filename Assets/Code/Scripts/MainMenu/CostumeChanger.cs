using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UICostumeChanger : MonoBehaviour
{
    public Image uiImage; // Obiekt UI (Image), który zmienia kostium
    public Sprite[] costumes; // Lista sprite'ów (kostiumów)
    public Sprite optionsCostume; // Kostium używany w "Options"
    public float changeInterval = 4f; // Czas między zmianami (sekundy)
    public float fadeDuration = 0.5f; // Czas trwania efektu zanikania

    private int currentIndex = 0;
    private bool isOptionsActive = false;
    private Coroutine costumeRoutine;

    void Start()
    {
        costumeRoutine = StartCoroutine(ChangeCostume());
    }

    public void EnterOptions()
    {
        isOptionsActive = true;
        if (costumeRoutine != null)
        {
            StopCoroutine(costumeRoutine); // Zatrzymujemy zmianę kostiumów
        }
        StartCoroutine(SetOptionsCostume()); // Ustawiamy jeden stały kostium z efektem fade
    }

    public void ExitOptions()
    {
        isOptionsActive = false;
        if (costumeRoutine != null)
        {
            StopCoroutine(costumeRoutine);
        }
        StartCoroutine(ResumeCostumeChange()); // Wznawiamy zmiany natychmiast
    }

    IEnumerator ChangeCostume()
    {
        while (!isOptionsActive)
        {
            yield return new WaitForSeconds(changeInterval);

            // Efekt zanikania (Fade Out)
            yield return StartCoroutine(FadeOut());

            // Zmiana kostiumu
            currentIndex = (currentIndex + 1) % costumes.Length;
            uiImage.sprite = costumes[currentIndex];

            // Efekt pojawiania się (Fade In)
            yield return StartCoroutine(FadeIn());
        }
    }

    IEnumerator SetOptionsCostume()
    {
        // Efekt zanikania (Fade Out)
        yield return StartCoroutine(FadeOut());

        // Ustawienie kostiumu dla Options
        uiImage.sprite = optionsCostume;

        // Efekt pojawiania się (Fade In)
        yield return StartCoroutine(FadeIn());
    }

    IEnumerator ResumeCostumeChange()
    {
        // Natychmiastowy efekt zanikania
        yield return StartCoroutine(FadeOut());

        // Zmiana na pierwszy kostium w kolejce
        currentIndex = 0;
        uiImage.sprite = costumes[currentIndex];

        // Efekt pojawiania się (Fade In)
        yield return StartCoroutine(FadeIn());

        // Wznowienie normalnego trybu zmiany
        costumeRoutine = StartCoroutine(ChangeCostume());
    }

    IEnumerator FadeOut()
    {
        float elapsedTime = 0f;
        Color startColor = uiImage.color;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(1, 0, elapsedTime / fadeDuration);
            uiImage.color = new Color(startColor.r, startColor.g, startColor.b, alpha);
            yield return null;
        }

        uiImage.color = new Color(startColor.r, startColor.g, startColor.b, 0);
    }

    IEnumerator FadeIn()
    {
        float elapsedTime = 0f;
        Color startColor = uiImage.color;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(0, 1, elapsedTime / fadeDuration);
            uiImage.color = new Color(startColor.r, startColor.g, startColor.b, alpha);
            yield return null;
        }

        uiImage.color = new Color(startColor.r, startColor.g, startColor.b, 1);
    }
}

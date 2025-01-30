using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UICostumeChanger : MonoBehaviour
{
    public Image uiImage; // Obiekt UI (Image), który zmienia kostium
    public Sprite[] costumes; // Lista sprite'ów (kostiumów)
    public float changeInterval = 4f; // Czas między zmianami (sekundy)
    public float fadeDuration = 0.5f; // Czas trwania efektu zanikania

    private int currentIndex = 0;

    void Start()
    {
        StartCoroutine(ChangeCostume());
    }

    IEnumerator ChangeCostume()
    {
        while (true)
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

using UnityEngine;
using TMPro;
using System.Collections;

public class FadeInTextMeshPro : MonoBehaviour
{
    public TextMeshProUGUI textMesh; // Przypisz TextMeshProUGUI w Inspectorze
    private float duration = 0.5f; // Czas trwania animacji

    void Start()
    {
        textMesh.color = new Color(textMesh.color.r, textMesh.color.g, textMesh.color.b, 0); // Ukryj tekst
        StartCoroutine(FadeIn());
    }

    IEnumerator FadeIn()
    {
        float elapsed = 0f;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float alpha = Mathf.Clamp01(elapsed / duration);
            textMesh.color = new Color(textMesh.color.r, textMesh.color.g, textMesh.color.b, alpha);
            yield return null;
        }
    }
}

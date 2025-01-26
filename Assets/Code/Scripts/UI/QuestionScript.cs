using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class QuestionScript : MonoBehaviour
{
    private TextMeshProUGUI textMeshPro;
    private Button AnswerOneBtn;
    private Button AnswerTwoBtn;
    private Button AnswerThreeBtn;

    private void Awake()
    {
        textMeshPro = transform.Find("Question").GetComponent<TextMeshProUGUI>();
        AnswerOneBtn = transform.Find("AnswerOneBtn").GetComponent<Button>();
        AnswerTwoBtn = transform.Find("AnswerTwoBtn").GetComponent<Button>();
        AnswerThreeBtn = transform.Find("AnswerThreeBtn").GetComponent<Button>();

        ShowQuestion("Are you sure u hate Juice?", () =>
        {
            Debug.Log("One");
        }, () =>
        {
            Debug.Log("Two");
        }, () =>
        {
            Debug.Log("Threee");
        });
    }

    public void ShowQuestion(string questionText, Action oneButton, Action twoButton, Action threeButton)
    {
        textMeshPro.text = questionText;
        AnswerOneBtn.onClick.AddListener(new UnityEngine.Events.UnityAction(oneButton));
        AnswerTwoBtn.onClick.AddListener(new UnityEngine.Events.UnityAction(twoButton));
        AnswerThreeBtn.onClick.AddListener(new UnityEngine.Events.UnityAction(threeButton));


    }
}

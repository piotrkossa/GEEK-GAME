using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class QuestionScript : MonoBehaviour
{
    public static QuestionScript Instance { get; private set;  }
    private TextMeshProUGUI textMeshPro;
    private Button AnswerOneBtn;
    private Button AnswerTwoBtn;
    private Button AnswerThreeBtn;

    private void Awake()
    {
        Instance = this;
        textMeshPro = transform.Find("Question").GetComponent<TextMeshProUGUI>();
        AnswerOneBtn = transform.Find("AnswerOneBtn").GetComponent<Button>();
        AnswerTwoBtn = transform.Find("AnswerTwoBtn").GetComponent<Button>();
        AnswerThreeBtn = transform.Find("AnswerThreeBtn").GetComponent<Button>();
        Hide(); 
    }

    public void ShowQuestion(string questionText, Action oneButton, Action twoButton, Action threeButton)
    {
        gameObject.SetActive(true);
        textMeshPro.text = questionText;
        AnswerOneBtn.onClick.AddListener(() =>
        {
            Hide();
            oneButton();
        });
        AnswerTwoBtn.onClick.AddListener(() =>
        {
            Hide();
            twoButton();
        });
        AnswerThreeBtn.onClick.AddListener(() =>
        {
            Hide();
            threeButton();
        });
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }
}

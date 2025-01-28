using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;
using UnityEngine.InputSystem;
using System.Collections;

public class Questiontest : MonoBehaviour
{
    [SerializeField] private GlobalVariables _globalVariables;
    public List<Questions> question = new List<Questions> {};
    private int currentIndex = 0;

    void Start()
    {
        Questions quest = new Questions("ktora rasa jest najgorsza", "Czarni", true, "Zydzi", true, "biali", false);
        question.Add(quest);
        StartCoroutine(ShowQuestionsCoroutine());
    }

    IEnumerator ShowQuestionsCoroutine()
    {
        while (currentIndex < question.Count)
        {
            var q = question[currentIndex];
            QuestionScript.Instance.ShowQuestion(
                q.questionText,
                q.answerA,
                q.answerB,
                q.answerC,
                () => OnAnswerChosen(q.isCorrectA),
                () => OnAnswerChosen(q.isCorrectB),
                () => OnAnswerChosen(q.isCorrectC)
            );
            currentIndex++;
            yield return new WaitForSeconds(15f);
        }
    }

    private void OnAnswerChosen(bool isCorrect)
    {
        if (isCorrect)
        {
            _globalVariables.UpdateMoney(100);
        }
        else
        {
            _globalVariables.UpdateMoney(-100);
        }

    }
}
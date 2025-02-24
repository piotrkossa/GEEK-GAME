using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;
using UnityEngine.InputSystem;
using System.Collections;

public class Questiontest : MonoBehaviour
{
    [SerializeField] private GlobalVariables _globalVariables;
    [SerializeField] List<Questions> question;
    private int currentIndex = 0;
    AudioManager audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    void Start()
    {
        StartCoroutine(ShowQuestionsCoroutine());
    }

    IEnumerator ShowQuestionsCoroutine()
    {
        while (currentIndex < question.Count)
        {
            yield return new WaitForSeconds(5f);
            if (_globalVariables.EnemyAmount == 0) continue;

            _globalVariables.isQuestioning = true;
            var q = question[currentIndex];
            bool answered = false;
            InputManager.Instance.BlockAllInput();
            audioManager.PlaySFX(audioManager.question);
            QuestionScript.Instance.ShowQuestion(
                q.questionText,
                q.answerA,
                q.answerB,
                q.answerC,
                () => { answered = true; OnAnswerChosen(q.isCorrectA); },
                () => { answered = true; OnAnswerChosen(q.isCorrectB); },
                () => { answered = true; OnAnswerChosen(q.isCorrectC); }
            );

            float remainingTime = 15f;
            while (remainingTime > 0f && !answered)
            {
                remainingTime -= Time.deltaTime;
                yield return null;
            }

            if (!answered)
            {
                OnAnswerChosen(false);
            }

            currentIndex++;
            InputManager.Instance.UnlockAllInput();
            _globalVariables.isQuestioning = false;
            yield return new WaitForSeconds(15f);
        }
    }

    private void OnAnswerChosen(bool isCorrect)
    {
        if (isCorrect)
        {
            audioManager.PlaySFX(audioManager.choice);
            _globalVariables.UpdateMoney(100);
            _globalVariables.UpdateCorrectAnswers();
        }
        else
        {
            audioManager.PlaySFX(audioManager.buyDeclined);
            _globalVariables.UpdateMoney(-100);
        }

    }
}
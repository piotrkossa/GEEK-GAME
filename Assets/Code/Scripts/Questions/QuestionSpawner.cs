using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;
using UnityEngine.InputSystem;
using System.Collections;
using UnityEngine.UI;

public class Questiontest : MonoBehaviour
{
    [SerializeField] private GlobalVariables _globalVariables;
    [SerializeField] List<Questions> question;
    [SerializeField] private GameObject feedbackPanel;
    private int alreadyAsked = 0;
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
        while (alreadyAsked < question.Count)
        {
            yield return new WaitForSeconds(5f);
            if (_globalVariables.EnemyAmount == 0) continue;

            _globalVariables.isQuestioning = true;

            int randomIndex = UnityEngine.Random.Range(0, question.Count);
            var q = question[randomIndex];
            question.RemoveAt(randomIndex);

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

            float remainingTime = 7.5f;
            while (remainingTime > 0f && !answered)
            {
                remainingTime -= Time.deltaTime;
                yield return null;
            }

            if (!answered)
            {
                OnAnswerChosen(false);
            }

            alreadyAsked++;
            InputManager.Instance.UnlockAllInput();
            _globalVariables.isQuestioning = false;
            yield return new WaitForSeconds(7.5f);
        }
    }

    private void OnAnswerChosen(bool isCorrect)
    {
        if (isCorrect)
        {
            audioManager.PlaySFX(audioManager.choice);
            Debug.Log("Correct");
            _globalVariables.UpdateMoney(75);
            _globalVariables.UpdateCorrectAnswers();
        }
        else
        {
            audioManager.PlaySFX(audioManager.buyDeclined);
            Debug.Log("Incorrect");
            _globalVariables.UpdateMoney(-50);
        }
        StartCoroutine(FlashPanelFeedback(isCorrect));

    }

    private IEnumerator FlashPanelFeedback(bool isCorrect)
    {
        feedbackPanel.SetActive(true);
        Image panelImage = feedbackPanel.GetComponent<Image>();

        Color targetColor = isCorrect ? new Color(0f, 1f, 0f, 0.2f) : new Color(1f, 0f, 0f, 0.2f);

        panelImage.color = new Color(targetColor.r, targetColor.g, targetColor.b, 0f);


        float fadeInDuration = 0.1f;
        float timer = 0f;
        while (timer < fadeInDuration)
        {
            timer += Time.deltaTime;
            float alpha = Mathf.Lerp(0f, targetColor.a, timer / fadeInDuration);
            panelImage.color = new Color(targetColor.r, targetColor.g, targetColor.b, alpha);
            yield return null;
        }

        yield return new WaitForSeconds(0.2f);

        float fadeOutDuration = 0.1f;
        timer = 0f;
        float startAlpha = panelImage.color.a;
        while (timer < fadeOutDuration)
        {
            timer += Time.deltaTime;
            float alpha = Mathf.Lerp(startAlpha, 0f, timer / fadeOutDuration);
            panelImage.color = new Color(targetColor.r, targetColor.g, targetColor.b, alpha);
            yield return null;
        }

        feedbackPanel.SetActive(false);
    }
}
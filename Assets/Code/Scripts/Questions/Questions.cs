using System.Collections.Generic;

public class Questions
{
    public string questionText;
    public string answerA;
    public bool isCorrectA;
    public string answerB;
    public bool isCorrectB;
    public string answerC;
    public bool isCorrectC;

    public Questions(string QuestionText, string AnswerA, bool IsCorrectA, string AnswerB, bool IsCorrectB, string AnswerC, bool IsCorrectC) {
        questionText = QuestionText;
        answerA = AnswerA;
        isCorrectA = IsCorrectA;
        answerB = AnswerB;
        isCorrectB = IsCorrectB;
        answerC = AnswerC;
        isCorrectC = IsCorrectC;
    }
}
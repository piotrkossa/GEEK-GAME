using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;
using UnityEngine.InputSystem;

public class Questiontest : MonoBehaviour
{

    private float timer = 0.0f;
    private bool answer = false;
    void Update()
    {


        if (timer >= 5)
        {
            QuestionScript.Instance.ShowQuestion("Do u hate juice?", "Tak", "Nie", "Nope", () =>
            {
                answer = true;
            }, () =>
            {
                answer = false;
            },
            () =>
            {
                answer = false;
            });
        }
    }
}

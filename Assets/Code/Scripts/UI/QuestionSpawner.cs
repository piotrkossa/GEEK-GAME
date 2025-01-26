using UnityEngine;

public class Questiontest : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            QuestionScript.Instance.ShowQuestion("Do u hate juice?", () =>
            {
                Debug.Log("1");
            }, () =>
            {
                Debug.Log("2");
            },
            () =>
            {
                Debug.Log("3");
            });
        }
    }
}

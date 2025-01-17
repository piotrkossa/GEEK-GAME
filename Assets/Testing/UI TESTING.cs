using UnityEngine;

public class UITESTING : MonoBehaviour
{
    public GlobalVariables _globalVariables;
    private float TimeUntilSecond = 0f;

    private void Update() {
        TimeUntilSecond += Time.deltaTime;
        if (TimeUntilSecond > 2f) {
            _globalVariables.UpdateHealth(1);
            _globalVariables.UpdateMoney(2);
            TimeUntilSecond = 0f;
        }
    }
}

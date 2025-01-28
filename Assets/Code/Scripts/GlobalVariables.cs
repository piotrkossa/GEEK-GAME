using UnityEngine;
using UnityEngine.Events;

public class GlobalVariables : MonoBehaviour
{
    public int Health {get; private set;}
    public int Money {get; private set;}

    public int CurrentLevelView {get; private set;}

    public UnityEvent onUpdated;


    public void UpdateHealth(int health) {
        if (Health + health < 0) {
            Health = 0;
            Debug.Log("GlobalVariables.cs: END GAME");
        }
        else
            Health += health;
        onUpdated.Invoke();
    }

    public void UpdateMoney(int money) {
        if (Money + money < 0) {
            Money = 0;
            Debug.Log("GlobalVariables.cs: MONEY ERROR");
        }
        else
            Money += money;
        onUpdated.Invoke();
    }

    public void UpdateCurrentLevelView(int level) {
        CurrentLevelView = level;
    }
}

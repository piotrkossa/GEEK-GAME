using UnityEngine;
using UnityEngine.Events;

public enum TurretTypes {
    Blue,
    Green,
    Red,
    Yellow
}

public class GlobalVariables : MonoBehaviour
{
    public int Health {get; private set;}
    public int Money {get; private set;}

    public int BlueAmount { get; private set; }
    public int GreenAmount { get; private set; }
    public int YellowAmount { get; private set; }
    public int RedAmount { get; private set; }

    public int CurrentLevelView {get; private set;}

    public UnityEvent onUiUpdated;
    public UnityEvent onViewChanged;
    public UnityEvent onTurretChanged;

    private void Awake() {
        Health = 100;
        Money = 100;
        CurrentLevelView = 0;

        BlueAmount = 1;
        GreenAmount = 0;
        RedAmount = 0;
        YellowAmount = 0;

    }

    public void UpdateHealth(int health) {
        if (Health + health < 0) {
            Health = 0;
            Debug.Log("GlobalVariables.cs: END GAME");
        }
        else
            Health += health;
        onUiUpdated.Invoke();
    }

    public void UpdateMoney(int money) {
        if (Money + money < 0) {
            Money = 0;
            Debug.Log("GlobalVariables.cs: MONEY ERROR");
        }
        else
            Money += money;
        onUiUpdated.Invoke();
    }

    public void UpdateBlue(int ammount)
    {
        BlueAmount += ammount;
        onTurretChanged.Invoke();
    }
    public void UpdateGreen(int ammount)
    {
        GreenAmount += ammount;
        onTurretChanged.Invoke();
    }
    public void UpdateYellow(int ammount)
    {
        YellowAmount += ammount;
        onTurretChanged.Invoke();
    }
    public void UpdateRed(int ammount)
    {
        RedAmount += ammount;
        onTurretChanged.Invoke();
    }

    public void UpdateCurrentLevelView(int level) {
        CurrentLevelView = level;
        onViewChanged.Invoke();
    }
}

using UnityEngine;
using UnityEngine.Events;

public class GlobalVariables : MonoBehaviour
{
    public int Health {get; private set;}
    public int Money {get; private set;}
    public int DefaultAmmount { get; private set; }
    public int PierceAmmount { get; private set; }
    public int FireAmmount { get; private set; }
    public int FastAmmount { get; private set; }

    public int CurrentLevelView {get; private set;}

    public UnityEvent onUiUpdated;
    public UnityEvent onViewChanged;

    private void Awake() {
        Health = 100;
        Money = 100;
        CurrentLevelView = 0;
        DefaultAmmount = 1;
        PierceAmmount = 0;
        FireAmmount = 0;

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

    public void UpdateDefault(int ammount)
    {
        DefaultAmmount += ammount;
    }
    public void UpdatePierce(int ammount)
    {
        PierceAmmount += ammount;
    }
    public void UpdateFire(int ammount)
    {
        FireAmmount += ammount;
    }
    public void UpdateFast(int ammount)
    {
        FastAmmount += ammount;
    }

    public void UpdateCurrentLevelView(int level) {
        CurrentLevelView = level;
        onViewChanged.Invoke();
    }
}

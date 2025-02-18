using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum TurretTypes {
    None,
    Blue,
    Green,
    Red,
    Yellow
}

public class GlobalVariables : MonoBehaviour
{
    public int Health {get; private set;}
    public int Money {get; private set;}

    public Dictionary<TurretTypes, int> TurretPrices {get; private set;} = new Dictionary<TurretTypes, int> {
        [TurretTypes.Blue] = 50,
        [TurretTypes.Green] = 100,
        [TurretTypes.Yellow] = 150,
        [TurretTypes.Red] = 200
    };

    public Dictionary<TurretTypes, int> TurretsOwned {get; private set;} = new Dictionary<TurretTypes, int> {
        [TurretTypes.Blue] = 1,
        [TurretTypes.Green] = 0,
        [TurretTypes.Yellow] = 0,
        [TurretTypes.Red] = 0
    };

    public int CurrentLevelView {get; private set;}

    public UnityEvent onUiUpdated;
    public UnityEvent onViewChanged;
    public UnityEvent onTurretChanged;

    private void Awake() {
        Health = 100;
        Money = 0;
        CurrentLevelView = 0;
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

    public bool UpdateMoney(int money) {
        if (Money + money < 0) {
            return false;
        }
        else
            Money += money;

        onUiUpdated.Invoke();
        return true;
    }

    public void UpdateTurrets(TurretTypes turretType, int amount)
    {
        TurretsOwned[turretType] += amount;
        onTurretChanged.Invoke();
    }

    public void UpdateCurrentLevelView(int level) {
        CurrentLevelView = level;
        onViewChanged.Invoke();
    }
}

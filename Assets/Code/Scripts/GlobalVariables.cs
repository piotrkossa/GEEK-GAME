using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public enum TurretTypes {
    None,
    Blue,
    Green,
    Red,
    Yellow
}

public class GlobalVariables : MonoBehaviour
{
    public static GlobalVariables Instance {get; private set;}

    public int Health {get; private set;}
    public int Money {get; private set;}
    public int CorrectAnswers { get; private set; }
    public int EnemyDefeated { get; private set; }


    private float timePlaying;
    [SerializeField] private GameObject helpScreen;
    public int EnemyAmount {get; private set;}
    public bool isQuestioning = false;

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

    public Dictionary<int, int> MonitorTurretCount {get; private set;} = new Dictionary<int, int> {
        [1] = 0,
        [2] = 0,
        [3] = 0
    };

    public int CurrentLevelView {get; private set;}

    public UnityEvent onUiUpdated;
    public UnityEvent onViewChanged;
    public UnityEvent onTurretChanged;
    public UnityEvent onMonitorTurretCountChanged;

    private void Awake() {
        Health = 100;
        Money = 300;
        CurrentLevelView = 0;
        EnemyAmount = 0;
    }

    private void Start() {
        Instance = this;
        if (PlayerPrefs.GetInt("FirstTime", 1) == 1)
        {

            helpScreen.SetActive(true);
            InputManager.Instance.BlockAllInput();
            Time.timeScale = 0;
            PlayerPrefs.SetInt("FirstTime", 0);
            PlayerPrefs.Save();
        }
    }

    private void Update()
    {
        timePlaying += Time.deltaTime;
        PlayerPrefs.SetFloat("TimePlaying", Mathf.RoundToInt(timePlaying));
    }

    public void UpdateHealth(int health) {
        if (Health + health < 0) {
            Health = 0;
            SceneManager.LoadScene("GameOver", LoadSceneMode.Single);
        }
        else
            Health += health;
        onUiUpdated.Invoke();
    }

    public void UpdateEnemyAmount(int n) {
        EnemyAmount += n;
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

    public void UpdateMonitorTurretCount() {
        MonitorTurretCount[CurrentLevelView] += 1;
        onMonitorTurretCountChanged.Invoke();
    }

    public bool CanPlaceTurret(int monitor) {
        int other1 = monitor % 3;
        int other2 = (monitor+1) % 3;
        other1++; other2++;

        if (MonitorTurretCount[monitor] - MonitorTurretCount[other1] >= 2 || MonitorTurretCount[monitor] - MonitorTurretCount[other2] >= 2)
            return false;
        
        return true;
    }

    public void UpdateCorrectAnswers()
    {
        CorrectAnswers++;
        PlayerPrefs.SetInt("CorrectAnswers", CorrectAnswers);
    }

    public void UpdateEnemyDefeated()
    {
        EnemyDefeated++;
        PlayerPrefs.SetInt("EnemyDefeated", EnemyDefeated);
    }
}

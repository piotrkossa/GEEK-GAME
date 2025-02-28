using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Threading.Tasks;
using System.Linq;


public enum EnemyTypes {
    Blue,
    Pink,
    Purple,
    Green,
    Yellow,
    Red
}

public class Level1 : MonoBehaviour
{
    AudioManager audioManager;

    [SerializeField] private EnemySpawner _enemySpawner;
    [SerializeField] private Level2 _level2;
    [SerializeField] private Waves _waveInfo;
    

    [Header("Enemies Prefab")]
    [SerializeField] private GameObject _blueEnemy;
    [SerializeField] private GameObject _pinkEnemy;
    [SerializeField] private GameObject _purpleEnemy;
    [SerializeField] private GameObject _greenEnemy;
    [SerializeField] private GameObject _yellowEnemy;
    [SerializeField] private GameObject _redEnemy;

    private Dictionary<EnemyTypes, GameObject> enemiesPrefabs;


    [Header("Phases")]
    [SerializeField] private float scalePercentPer4Sec;
    [SerializeField] private Phase phase1;
    [SerializeField] private Phase phase2;
    [SerializeField] private Phase phase3;

    private Phase[] phases = new Phase [3];

    [Serializable]
    private struct Enemy {
        public EnemyTypes enemyType;
        public float interval; // jedynie wielokrotnosci 0.5f
    }

    [Serializable]
    private struct Phase {
        public int duration;
        public Enemy[] enemies;
        public bool isScaling;
    }

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    private void Start() {
        FillDictionaries();
        _enemySpawner.OnEnemyDone.AddListener(EnemyDone);
        StartCoroutine(ManagePhases());
    }

    private IEnumerator ManagePhases() {
        InputManager.Instance.BlockAllInput();
        if (PlayerPrefs.GetInt("FirstTime", 1) == 1)
            yield return new WaitForSeconds(1f);
        else
            yield return new WaitForSeconds(5f);
        
        InputManager.Instance.UnlockAllInput();
        
        yield return StartCoroutine(_waveInfo.ShowWaveInfo(0));
        yield return new WaitForSeconds(2f);

        yield return PerformPhase(phases[0]);

        yield return new WaitForSeconds(3f);

        yield return StartCoroutine(_waveInfo.ShowWaveInfo(1));
        audioManager.PlayWaveMusic(1);
        yield return new WaitForSeconds(3f);

        yield return PerformPhase(phases[1]);

        Debug.Log("czekam");

        yield return new WaitForSeconds(3f);

        Debug.Log("nie czekam");

        yield return StartCoroutine(_waveInfo.ShowWaveInfo(2));
        audioManager.PlayWaveMusic(2);
        yield return new WaitForSeconds(3f);

        yield return PerformPhase(phases[2]);
    }

    private float phaseScale;

    private IEnumerator PerformPhase(Phase phase) {
        float delay = 0f;
        phaseScale = 1f;

        List<Coroutine> coroutines = new List<Coroutine> {};
        foreach (Enemy enemy in phase.enemies) {
            coroutines.Add(StartCoroutine(SpawnCoroutine(enemy.enemyType, delay, enemy.interval)));
            delay += 0.15f;
        }
        if (phase.isScaling)
            coroutines.Add(StartCoroutine(Scaler()));

        yield return new WaitForSeconds(phase.duration);
        
        foreach (Coroutine coroutine in coroutines) {
            StopCoroutine(coroutine);
        }

        while (true) {
            yield return new WaitForSeconds(1f);
            if (GameObject.FindWithTag("Enemy") == null && GlobalVariables.Instance.isQuestioning == false) {
                break;
            }
        }
    }

    private IEnumerator Scaler() {
        while (true) {
            yield return new WaitForSeconds(4f);
            phaseScale += scalePercentPer4Sec;
        }
    }

    private IEnumerator SpawnCoroutine(EnemyTypes enemyType, float startDelay, float interval) {
        yield return new WaitForSeconds(startDelay);

        while (true) {
            _enemySpawner.SpawnEnemy(enemiesPrefabs[enemyType]);
            yield return new WaitForSeconds(interval * (1/phaseScale));
        }
    }

    public void EnemyDone(GameObject enemyPrefab) {
        _level2.EnemyEntered(enemyPrefab);
    }

    private void FillDictionaries() {
        phases[0] = phase1; phases[1] = phase2; phases[2] = phase3;
        enemiesPrefabs = new Dictionary<EnemyTypes, GameObject> {
            [EnemyTypes.Blue] = _blueEnemy,
            [EnemyTypes.Pink] = _pinkEnemy,
            [EnemyTypes.Purple] = _purpleEnemy,
            [EnemyTypes.Green] = _greenEnemy,
            [EnemyTypes.Yellow] = _yellowEnemy,
            [EnemyTypes.Red] = _redEnemy
        };
    }
}

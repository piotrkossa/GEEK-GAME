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
    [SerializeField] private EnemySpawner _enemySpawner;
    [SerializeField] private Level2 _level2;
    

    [Header("Enemies Prefab")]
    [SerializeField] private GameObject _blueEnemy;
    [SerializeField] private GameObject _pinkEnemy;
    [SerializeField] private GameObject _purpleEnemy;
    [SerializeField] private GameObject _greenEnemy;
    [SerializeField] private GameObject _yellowEnemy;
    [SerializeField] private GameObject _redEnemy;

    private Dictionary<EnemyTypes, GameObject> enemiesPrefabs;


    [Header("Phases")]
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
    }

    private void Start() {
        FillDictionaries();
        _enemySpawner.OnEnemyDone.AddListener(EnemyDone);
        ManagePhases();
    }

    private async void ManagePhases() {
        await PerformPhase(phases[0]);
        await PerformPhase(phases[1]);
        await PerformPhase(phases[2]);
    }

    private async Task PerformPhase(Phase phase) {
        float delay = 0f;
        foreach (Enemy enemy in phase.enemies) {
            StartCoroutine(SpawnCoroutine(enemy.enemyType, delay, enemy.interval));
            delay += 0.3f;
        }

        await Awaitable.WaitForSecondsAsync(phase.duration);
        StopAllCoroutines();
        while (true) {
            await Awaitable.WaitForSecondsAsync(1f);
            if (GlobalVariables.Instance.EnemyAmount == 0) {
                break;
            }
        }
    }

    private IEnumerator SpawnCoroutine(EnemyTypes enemyType, float startDelay, float interval) {
        yield return new WaitForSeconds(startDelay);
        while (true) {
            _enemySpawner.SpawnEnemy(enemiesPrefabs[enemyType]);
            yield return new WaitForSeconds(interval);
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

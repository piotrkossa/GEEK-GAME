using UnityEngine;
using System.Collections;

public class Level1 : MonoBehaviour
{
    [SerializeField] private EnemySpawner _enemySpawner;
    [SerializeField] private Level2 _level2;
    
    [SerializeField] private float enemySpawnDelay;

    private void Start() {
        StartCoroutine(EnemySpawnCoroutine());
        _enemySpawner.OnEnemyDone.AddListener(EnemyDone);
    }

    private IEnumerator EnemySpawnCoroutine() {
        while (true) {
            yield return new WaitForSeconds(enemySpawnDelay);
            _enemySpawner.SpawnEnemy();
        }
    }

    public void EnemyDone() {
        _level2.EnemyEntered();
    }
}

using UnityEngine;

public class Level2 : MonoBehaviour
{
    [SerializeField] private EnemySpawner _enemySpawner;
    [SerializeField] private Level3 _level3;

    void Start()
    {
        _enemySpawner.OnEnemyDone.AddListener(EnemyDone);
    }

    private void EnemyDone(GameObject enemyPrefab) {
        _level3.EnemyEntered(enemyPrefab);
    }

    public void EnemyEntered(GameObject enemyPrefab) {
        _enemySpawner.SpawnEnemy(enemyPrefab);
    }
}

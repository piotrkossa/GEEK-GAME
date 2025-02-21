using UnityEngine;

public class Level3 : MonoBehaviour
{
    [SerializeField] private EnemySpawner _enemySpawner;
    [SerializeField] private GlobalVariables _globalVariables;
    [SerializeField] private int healthPenalty;

    void Start()
    {
        _enemySpawner.OnEnemyDone.AddListener(EnemyDone);
    }

    private void EnemyDone(GameObject _) {
        _globalVariables.UpdateHealth(healthPenalty);
    }

    public void EnemyEntered(GameObject enemyPrefab) {
        _enemySpawner.SpawnEnemy(enemyPrefab);
    }
}

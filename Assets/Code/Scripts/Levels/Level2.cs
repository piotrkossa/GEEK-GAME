using UnityEngine;

public class Level2 : MonoBehaviour
{
    [SerializeField] private EnemySpawner _enemySpawner;
    [SerializeField] private Level3 _level3;

    void Start()
    {
        _enemySpawner.OnEnemyDone.AddListener(EnemyDone);
    }

    private void EnemyDone() {
        _level3.EnemyEntered();
    }

    public void EnemyEntered() {
        _enemySpawner.SpawnEnemy();
    }
}

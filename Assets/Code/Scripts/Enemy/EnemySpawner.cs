using UnityEngine;
using UnityEngine.Events;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject _enemyPrefab;

    [SerializeField] private EnemyPath _enemyPath;

    public UnityEvent OnEnemyDone;

    private Vector3[] path;

    private void Start() {
        path = _enemyPath.GetPath();
    }
    
    public void SpawnEnemy() {
        Object.Instantiate(_enemyPrefab, this.transform);
    }

    public void PathCompleted() {
        OnEnemyDone.Invoke();
    }

    public Vector3[] GetPath() {
        return path;
    }
}

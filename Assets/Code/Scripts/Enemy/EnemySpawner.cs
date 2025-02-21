using UnityEngine;
using UnityEngine.Events;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private EnemyPath _enemyPath;
    [SerializeField] private GlobalVariables _globalVariables;

    public UnityEvent<GameObject> OnEnemyDone;

    private Vector3[] path;

    private void Start() {
        path = _enemyPath.GetPath();
    }
    
    public void SpawnEnemy(GameObject _enemyPrefab) {
        Object.Instantiate(_enemyPrefab, this.transform);
    }

    public void PathCompleted(GameObject enemyPrefab) {
        OnEnemyDone.Invoke(enemyPrefab);
    }

    public Vector3[] GetPath() {
        return path;
    }

    public GlobalVariables GetGlobalVariables() {
        return _globalVariables;
    }
}

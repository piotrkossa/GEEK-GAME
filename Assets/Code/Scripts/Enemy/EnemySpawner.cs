using UnityEngine;
using UnityEngine.PlayerLoop;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject _enemyPrefab;

    [SerializeField] private EnemyPath _enemyPath;
    private Vector3[] path;

    private void Start() {
        path = _enemyPath.GetPath();
        InvokeRepeating("SpawnEnemy", 0f, 3f);
    }
    
    private void SpawnEnemy() {
        Object.Instantiate(_enemyPrefab, this.transform);
    }

    public void PathCompleted() {
        Debug.Log("-1 HP");
    }

    public Vector3[] GetPath() {
        return path;
    }
}

using System.Runtime.CompilerServices;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float speed;

    private EnemySpawner _enemySpawner;
    private Vector3[] pathPoints;

    private int pointsCount;
    private int currentGoal = 0;


    private void Start() {
        _enemySpawner = this.transform.parent.gameObject.GetComponent<EnemySpawner>();
        
        pathPoints = _enemySpawner.GetPath();
        this.transform.position = pathPoints[0];
        pointsCount = pathPoints.Length;
    }

    private void Update() {
        if (this.transform.position == pathPoints[currentGoal]) {
            currentGoal += 1;
            if (currentGoal == pointsCount) {
                _enemySpawner.PathCompleted(this.gameObject);
                Destroy(this.gameObject);
                return;
            }
        }
        this.transform.position = Vector3.MoveTowards(this.transform.position, pathPoints[currentGoal], Time.deltaTime * speed);
    }
}

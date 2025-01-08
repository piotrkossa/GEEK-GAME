using UnityEngine;

public class EnemyPath : MonoBehaviour
{
    private Vector3[] points;

    private void Awake() {
        points = new Vector3 [this.transform.childCount];

        for (int i = 0; i < this.transform.childCount; i++) {
            points[i] = this.transform.GetChild(i).GetComponent<Transform>().position;
        }
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.blue;
        Vector3 last = this.transform.GetChild(0).GetComponent<Transform>().position;
        Vector3 current;
        for (int i = 1; i < this.transform.childCount; i++) {
            current = this.transform.GetChild(i).GetComponent<Transform>().position;
            Gizmos.DrawLine(last, current);
            last = current;
        }
    }

    public Vector3[] GetPath() {
        return points;
    }
}

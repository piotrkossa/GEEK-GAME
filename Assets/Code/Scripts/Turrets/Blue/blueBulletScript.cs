using System.Net.NetworkInformation;
using UnityEngine;

public class blueBulletScript : MonoBehaviour
{

    private Transform target;

    [Header("References")]
    [SerializeField] private Rigidbody2D rb;

    [Header("Attributes")]
    [SerializeField] private float bulletSpeed = 5f;
    [SerializeField] private int bulletDamage = 1;
    [SerializeField] private float maxDistance = 3f;

    private Vector3 startingPosition;
    public void SetTarget(Transform _target)
    {
        target = _target;
    }

    private void FixedUpdate() // przesuwanie kulki
    {
        if (!target) return;
 
        Vector2 direction = (target.position - transform.position).normalized;

        rb.linearVelocity = direction * bulletSpeed; //leci w strone przeciwnik zeby nie missowac zbytnio
    }

    private void OnCollisionEnter2D(Collision2D other) // zadaje obrazenie i usuwa kulke
    {
        GameObject _enemyObject = other.gameObject;
        Debug.Log(_enemyObject.tag);
        if (_enemyObject.CompareTag("Enemy")) {
            _enemyObject.GetComponent<EnemyHealth>().TakeDamage(bulletDamage);
            Destroy(gameObject);
        }
        else if (_enemyObject.CompareTag("Monitor border"))
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        startingPosition = transform.position;
    }

    private void Update()
    {
        float traveledDistance = Vector3.Distance(startingPosition, transform.position);
        if (traveledDistance >= maxDistance)
        {
            Destroy(gameObject);
        }
    }
}

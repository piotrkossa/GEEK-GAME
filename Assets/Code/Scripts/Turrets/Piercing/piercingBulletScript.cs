using UnityEngine;

public class bulletScript1 : MonoBehaviour
{

    private Transform target;

    [Header("References")]
    [SerializeField] private Rigidbody2D rb;

    [Header("Attributes")]
    [SerializeField] private float bulletSpeed = 5f;
    [SerializeField] private int bulletDamage = 1;
    [SerializeField] private int maxPiercingAmmount = 2;
    private int pierced = 0;
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
        if (_enemyObject.CompareTag("Enemy")) {
            if (pierced < maxPiercingAmmount)
            {
                _enemyObject.GetComponent<EnemyHealth>().TakeDamage(bulletDamage);
                pierced++;
            }
            else
            {
                _enemyObject.GetComponent<EnemyHealth>().TakeDamage(bulletDamage);
                Destroy(gameObject);
                pierced = 0;
            }
        }
    }
}

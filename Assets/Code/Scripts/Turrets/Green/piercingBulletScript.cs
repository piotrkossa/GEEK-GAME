using UnityEngine;

public class piercingBulletScript : MonoBehaviour
{
    private Vector2 direction;

    [Header("References")]
    [SerializeField] private Rigidbody2D rb;

    [Header("Attributes")]
    [SerializeField] private float bulletSpeed = 5f;
    [SerializeField] private int bulletDamage = 1;
    [SerializeField] private int maxPiercingAmount = 2;

    private int pierced = 0;

    // Ustawiamy kierunek lotu na podstawie celu
    public void SetTarget(Transform target)
    {
        if (target != null)
        {
            direction = (target.position - transform.position).normalized;
        }
        else
        {
            direction = transform.right; // W razie braku celu, leci prosto
        }
    }

    private void Start()
    {
        rb.linearVelocity = direction * bulletSpeed;
        RotateTowardsDirection();
    }

    private void RotateTowardsDirection()
    {
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            EnemyHealth enemyHealth = other.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(bulletDamage);
                pierced++;

                Debug.Log("Hit: " + pierced);

                if (pierced >= maxPiercingAmount)
                {
                    Destroy(gameObject);
                }
            }
        }
        else if (other.CompareTag("Monitorborder"))
        {
            Destroy(gameObject);
        }
    }
}

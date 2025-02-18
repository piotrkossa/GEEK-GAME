using UnityEngine;

public class yellowBulletScript : MonoBehaviour
{
    private Transform target;
    private bool useCustomDirection = false;
    private Vector2 customDirection;

    [Header("References")]
    [SerializeField] private Rigidbody2D rb;

    [Header("Attributes")]
    [SerializeField] private float bulletSpeed = 5f;
    [SerializeField] private int bulletDamage = 1;


    // Ustawienie celu – pocisk bêdzie œledzi³ pozycjê przeciwnika
    public void SetTarget(Transform _target)
    {
        target = _target;
    }

    // Ustawienie w³asnego kierunku – przydatne dla pocisków lec¹cych pod k¹tem
    public void SetDirection(Vector2 direction)
    {
        customDirection = direction.normalized;
        useCustomDirection = true;
    }

    private void FixedUpdate() // Poruszanie pociskiem
    {
        // Jeœli nie mamy ustawionego celu ani kierunku – nie rób nic
        if (!target && !useCustomDirection) return;

        Vector2 direction;
        if (useCustomDirection)
        {
            direction = customDirection;
        }
        else
        {
            direction = ((Vector2)target.position - (Vector2)transform.position).normalized;
        }

        rb.linearVelocity = direction * bulletSpeed; // Ustawiamy prêdkoœæ w wyliczonym kierunku
    }

    private void OnCollisionEnter2D(Collision2D other) // Zadaje obra¿enia i niszczy pocisk
    {
        GameObject _enemyObject = other.gameObject;
        if (_enemyObject.CompareTag("Enemy"))
        {
            _enemyObject.GetComponent<EnemyHealth>().TakeDamage(bulletDamage);
            Destroy(gameObject);
        }
        if (_enemyObject.CompareTag("Monitorborder"))
        {
            Destroy(gameObject);
        }
    }
}

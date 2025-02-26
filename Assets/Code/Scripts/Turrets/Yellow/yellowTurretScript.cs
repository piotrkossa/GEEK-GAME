using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;

public class yellowTurretScript : MonoBehaviour
{
    [Header("Refrences")]
    [SerializeField] private Transform turretRotationPoint;
    [SerializeField] private LayerMask enemyMask;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firingPoint;

    [Header("Attribute")]
    [SerializeField] private float targetingRange = 5f;
    [SerializeField] private float rotationSpeed = 5f;
    [SerializeField] private float bulletsPerSecond = 1f; // Iloœæ pocisków na sekundê

    private Transform target;
    private float timeUntilFire;
    AudioManager audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    private void Update() // Wybór celu, sprawdzanie zasiêgu i strzelanie
    {
        if (target == null) // Jeœli brak celu – poszukaj nowego
        {
            FindTarget();
            return;
        }

        bool isPointing = RotateTowardsTarget();

        if (!CheckTargetIsInRange())
        {
            target = null;
        }
        else
        {
            timeUntilFire += Time.deltaTime;

            if (timeUntilFire >= 1f / bulletsPerSecond && isPointing)
            {
                Shoot();
                timeUntilFire = 0f;
            }
        }
    }

    private void Shoot()  // Tworzy trzy pociski: jeden œledz¹cy cel i dwa pod k¹tem 45°
    {
        // Obliczamy bazowy kierunek od punktu wystrza³u do celu
        Vector2 baseDirection = ((Vector2)target.position - (Vector2)firingPoint.position).normalized;

        audioManager.PlaySFX(audioManager.shoot);

        // Pocisk 1: lec¹cy dok³adnie w stronê przeciwnika (œledzenie)
        GameObject bullet1 = Instantiate(bulletPrefab, firingPoint.position, Quaternion.identity);
        yellowBulletScript bulletScript1 = bullet1.GetComponent<yellowBulletScript>();
        bulletScript1.SetTarget(target);

        // Pocisk 2: lec¹cy pod k¹tem +45° wzglêdem bazowego kierunku
        Vector2 leftDirection = Quaternion.Euler(0, 0, 45) * baseDirection;
        GameObject bullet2 = Instantiate(bulletPrefab, firingPoint.position, Quaternion.identity);
        yellowBulletScript bulletScript2 = bullet2.GetComponent<yellowBulletScript>();
        bulletScript2.SetDirection(leftDirection);

        // Pocisk 3: lec¹cy pod k¹tem -45° wzglêdem bazowego kierunku
        Vector2 rightDirection = Quaternion.Euler(0, 0, -45) * baseDirection;
        GameObject bullet3 = Instantiate(bulletPrefab, firingPoint.position, Quaternion.identity);
        yellowBulletScript bulletScript3 = bullet3.GetComponent<yellowBulletScript>();
        bulletScript3.SetDirection(rightDirection);
    }

    private void FindTarget() // Szukanie pierwszego przeciwnika za pomoc¹ CircleCastAll
    {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, targetingRange, (Vector2)transform.position, 0f, enemyMask);

        if (hits.Length > 0)
        {
            target = hits[0].transform;
        }
    }

    private bool RotateTowardsTarget() // Obracanie wie¿yczki w kierunku celu
    {
        float desiredAngle = Mathf.Atan2(target.position.y - transform.position.y, target.position.x - transform.position.x) * Mathf.Rad2Deg - 90f;
        Quaternion desiredRotation = Quaternion.Euler(new Vector3(0f, 0f, desiredAngle));

        turretRotationPoint.rotation = Quaternion.RotateTowards(turretRotationPoint.rotation, desiredRotation, rotationSpeed * Time.deltaTime);

        if (Quaternion.Angle(turretRotationPoint.rotation, desiredRotation) < 0.2f)
            return true;

        return false;
    }

    private bool CheckTargetIsInRange()
    {
        return Vector2.Distance(target.position, transform.position) <= targetingRange;
    }

    private void OnDrawGizmosSelected() // Rysowanie zasiêgu w edytorze Unity
    {
#if UNITY_EDITOR
        Handles.color = Color.cyan;
        Handles.DrawWireDisc(transform.position, transform.forward, targetingRange);
#endif
    }
}

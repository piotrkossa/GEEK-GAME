using UnityEngine;

public class blueTurretScript : MonoBehaviour
{
    [Header("Refrences")]
    [SerializeField] private Transform turretRotationPoint;
    [SerializeField] private LayerMask enemyMask;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firingPoint;

    [Header("Attribute")]
    [SerializeField] private float targetingRange = 5f;
    [SerializeField] private float rotationSpeed = 5f;
    [SerializeField] private float bulletsPerSecond = 1f;

    private Transform target;
    private float timeUntilFire;
    private float lastFireTime;
    AudioManager audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    private void Update()
    {
        if (target == null)
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
            if (isPointing && Time.time - lastFireTime >= 1f / bulletsPerSecond)
            {
                Shoot();
                lastFireTime = Time.time;
            }
        }
    }

    private void Shoot()
    {
        GameObject _bulletObject = Instantiate(bulletPrefab, firingPoint.position, Quaternion.identity);
        audioManager.PlaySFX(audioManager.shoot);
        blueBulletScript _bulletScript = _bulletObject.GetComponent<blueBulletScript>();
        _bulletScript.SetTarget(target);
    }

    private void FindTarget()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, targetingRange, enemyMask);
        float closestDistance = Mathf.Infinity;
        target = null;

        foreach (Collider2D hit in hits)
        {
            float distanceToTarget = Vector2.Distance(transform.position, hit.transform.position);
            if (distanceToTarget < closestDistance)
            {
                closestDistance = distanceToTarget;
                target = hit.transform;
            }
        }
    }

    private bool RotateTowardsTarget()
    {
        if (target == null) return false;

        Vector3 direction = target.position - turretRotationPoint.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;

        Quaternion desiredRotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
        turretRotationPoint.rotation = Quaternion.RotateTowards(turretRotationPoint.rotation, desiredRotation, rotationSpeed * Time.deltaTime);

        return Quaternion.Angle(turretRotationPoint.rotation, desiredRotation) < 0.2f;
    }

    private bool CheckTargetIsInRange()
    {
        return target != null && Vector2.Distance(target.position, transform.position) <= targetingRange;
    }

    private void OnDrawGizmosSelected()
    {
#if UNITY_EDITOR
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, targetingRange);
#endif
    }
}

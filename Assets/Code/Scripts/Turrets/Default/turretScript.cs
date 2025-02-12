using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;

public class turretScript : MonoBehaviour
{
    [Header("Refrences")]
    [SerializeField] private Transform turretRotationPoint;
    [SerializeField] private LayerMask enemyMask;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firingPoint; 

    [Header("Attribute")]
    [SerializeField] private float targetingRange = 5f;
    [SerializeField] private float rotationSpeed = 5f;
    [SerializeField] private float bulletsPerSecond = 1f; // Bullets per second
    private Transform target;
    private float timeUntilFire;
    private void Update() //wybranie celu sprawdzenie czy w zasiegu i strzal
    {
        if (target == null) //obranie nowego celu
        {
            FindTarget();
            return;
        }

        RotateTowardsTarget();

        if (!CheckTargetIsInRange())
        {
            target = null;
        }
        else
        {
            timeUntilFire += Time.deltaTime;

            if (timeUntilFire >= 1f/bulletsPerSecond)
            {
                Shoot();
                timeUntilFire = 0f;
            }
        }
    }

    private void Shoot()  //tworzy pocisk w firingpoint i ustawia cel
    {
        GameObject _bulletObject = Instantiate(bulletPrefab, firingPoint.position, Quaternion.identity);
        bulletScript _bulletScript = _bulletObject.GetComponent<bulletScript>();
        _bulletScript.SetTarget(target);
    }

    private void FindTarget() // szukanie pierwszego przeciwnika przez raycast w okregu
    {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, targetingRange, (Vector2)transform.position, 0f, enemyMask);
        // odwoluj sie do maski aby zidentyfikowac ze to enemy a nie cos innego
        
        if (hits.Length > 0)
        {
            target = hits[0].transform;
        }
    }

    private void RotateTowardsTarget() // wyliczamy kat i przesuwamy gun w kierunku enemy
    {
        float angle = Mathf.Atan2(target.position.y - transform.position.y, target.position.x - transform.position.x) * Mathf.Rad2Deg - 90f;

        Quaternion targetRotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
        turretRotationPoint.rotation = Quaternion.RotateTowards(turretRotationPoint.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    private bool CheckTargetIsInRange()
    {
        return Vector2.Distance(target.position, transform.position) <= targetingRange;
    }

    private void OnDrawGizmosSelected() // widac range jak siedzisz w unity
    {
#if UNITY_EDITOR
        Handles.color = Color.cyan;
        Handles.DrawWireDisc(transform.position, transform.forward, targetingRange);
#endif
    }
}

using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private GlobalVariables _globalVariables;
    private EnemySpawner _enemySpawner;
    [Header("Attributes")]
    [SerializeField] private int hitPoints = 2; //ilosc zycia

    private void Start() {
        _enemySpawner = this.transform.parent.gameObject.GetComponent<EnemySpawner>();
        _globalVariables = _enemySpawner.GetGlobalVariables();
    }

    public void TakeDamage(int dmg) //zadanie obrazenia w bulletScript
    {
        hitPoints -= dmg; 

        if (hitPoints <= 0)
        {
            _globalVariables.UpdateEnemyAmount(-1);
            Destroy(gameObject);
        }
    }
}

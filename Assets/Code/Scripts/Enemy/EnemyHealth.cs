using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [Header("Attributes")]
    [SerializeField] private int hitPoints = 2; //ilosc zycia

    public void TakeDamage(int dmg) //zadanie obrazenia w bulletScript
    {
        hitPoints -= dmg; 

        if (hitPoints <= 0)
        {
            Destroy(gameObject);
        }
    }
}

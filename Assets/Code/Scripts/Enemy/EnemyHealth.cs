using TMPro;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [Header("Attributes")]
    [SerializeField] private int hitPoints; //ilosc zycia
    private int maxPoints;

    private SpriteRenderer spriteRenderer;

    private void Start() {
        spriteRenderer = this.GetComponent<SpriteRenderer>();
        maxPoints = hitPoints;
    }

    public void TakeDamage(int dmg) //zadanie obrazenia w bulletScript
    {
        hitPoints -= dmg;

        spriteRenderer.color = new Color(1, 1, 1, (float)hitPoints/(float)maxPoints);

        if (hitPoints <= 0)
        {
            GlobalVariables.Instance.UpdateEnemyAmount(-1);
            Destroy(gameObject);
        }
    }
}

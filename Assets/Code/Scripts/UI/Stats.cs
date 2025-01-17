using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class Stats : MonoBehaviour
{
    [SerializeField] private GlobalVariables _globalVariables;

    [Header("Stats Objects")]
    [SerializeField] private TextMeshProUGUI _health;
    [SerializeField] private TextMeshProUGUI _money;

    private void Awake() {
        _globalVariables.onUpdated.AddListener(UpdateValues);
    }

    private void UpdateValues() {
        _health.text = _globalVariables.Health.ToString();
        _money.text = _globalVariables.Money.ToString();
    }
}

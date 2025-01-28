using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class Stats : MonoBehaviour
{
    private GlobalVariables _globalVariables;

    [Header("Stats Objects")]
    [SerializeField] private TextMeshProUGUI _health;
    [SerializeField] private TextMeshProUGUI _money;

    private void Awake() {
        MainUI _UI = this.transform.parent.GetComponent<MainUI>();
        _globalVariables = _UI.GetGlobalVariables();

        _globalVariables.onUiUpdated.AddListener(UpdateValues);
    }

    private void UpdateValues() {
        _health.text = _globalVariables.Health.ToString();
        _money.text = _globalVariables.Money.ToString();
    }
}

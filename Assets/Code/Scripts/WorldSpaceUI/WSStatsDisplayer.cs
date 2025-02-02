using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WSStatsDisplayer : MonoBehaviour
{
    [SerializeField] private GlobalVariables _globalVariables;

    [SerializeField] private Slider _healthBar;

    [SerializeField] private TextMeshProUGUI _money;

    private void Awake() {
        _globalVariables.onUiUpdated.AddListener(UpdateValues);
    }

    private void UpdateValues() {
        _healthBar.value = _globalVariables.Health / 100f;
        _money.text = _globalVariables.Money.ToString() + " $";
    }
}

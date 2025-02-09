using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WSStatsDisplayer : MonoBehaviour
{
    [SerializeField] private GlobalVariables _globalVariables;
    [SerializeField] private TransitionManager _transitionManager;

    [SerializeField] private Slider _healthBar;
    private float maxHealth;

    [SerializeField] private TextMeshProUGUI _money;

    private void Start() {
        _globalVariables.onUiUpdated.AddListener(UpdateValues);
        maxHealth = _globalVariables.Health;
        _money.text = _globalVariables.Money.ToString();
    }

    private void UpdateValues() {
        float hbValue = _globalVariables.Health / maxHealth;
        if (hbValue < 0.125f && hbValue > 0f) hbValue = 0.125f;
        hbValue = hbValue - (hbValue % 0.125f);
        ChangeHealth(hbValue);
        _money.text = _globalVariables.Money.ToString();
    }

    private bool isTrans = false;

    private async void ChangeHealth(float t) {
        if (isTrans) return;

        isTrans = true;

        await _transitionManager.SliderValueTransition(_healthBar, _healthBar.value, t, 0.5f, 0.67f, 0.72f);

        isTrans = false;
    }
}

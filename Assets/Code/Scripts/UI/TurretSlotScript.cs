using TMPro;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class TurretSlotScript : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private TurretTypes turretType;

    [Header("External Requirements")]
    [SerializeField] private GlobalVariables _globalVariables;

    [Header("Internal Requirements")]
    [SerializeField] private TextMeshProUGUI _turretCountText;

    private void Start() {
        _globalVariables.onTurretChanged.AddListener(UpdateTurrets);
        this.GetComponent<Button>().onClick.AddListener(OnClick);
        UpdateTurrets();
    }

    private void UpdateTurrets() {
        switch (turretType) {
            case TurretTypes.Blue:
                _turretCountText.text = _globalVariables.BlueAmount.ToString();
                break;
            case TurretTypes.Green:
                _turretCountText.text = _globalVariables.GreenAmount.ToString();
                break;
            case TurretTypes.Yellow:
                _turretCountText.text = _globalVariables.YellowAmount.ToString();
                break;
            case TurretTypes.Red:
                _turretCountText.text = _globalVariables.RedAmount.ToString();
                break;
        }
    }

    private void OnClick() {
        this.transform.parent.GetComponent<TurretSpawner>().TurretChoosen(turretType);
    }
}

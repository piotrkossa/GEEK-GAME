using TMPro;
using UnityEngine;
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
        _turretCountText.text = _globalVariables.TurretsOwned[turretType].ToString();
    }

    private void OnClick() {
        if (_globalVariables.TurretsOwned[turretType] > 0)
            this.transform.parent.GetComponent<TurretSpawner>().TurretChoosen(turretType);
        else
            Debug.Log("TurretSlotScript.cs: Nie masz juz turretow LLL");
    }
}

using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Shelved : MonoBehaviour
{

    [SerializeField] private GlobalVariables _globalVariables;

    [Header("Buttons")]
    [SerializeField] private Button _blueButton;
    [SerializeField] private Button _greenButton;
    [SerializeField] private Button _yellowButton;
    [SerializeField] private Button _redButton;

    [Header("Icons")]
    [SerializeField] private Image _blueIcon;
    [SerializeField] private Image _greenIcon;
    [SerializeField] private Image _yellowIcon;
    [SerializeField] private Image _redIcon;

    private Dictionary<TurretTypes, Image> iconImages = new Dictionary<TurretTypes, Image> {};


    private void Start() {
        _blueButton.onClick.AddListener(delegate{ButtonClicked(TurretTypes.Blue);});
        _greenButton.onClick.AddListener(delegate{ButtonClicked(TurretTypes.Green);});
        _yellowButton.onClick.AddListener(delegate{ButtonClicked(TurretTypes.Yellow);});
        _redButton.onClick.AddListener(delegate{ButtonClicked(TurretTypes.Red);});

        SetPrices();
        DictSetup();
    }


    private void ButtonClicked(TurretTypes turretType) {
        if (_globalVariables.UpdateMoney(-_globalVariables.TurretPrices[turretType]))
            PurchaseAccepted(turretType);
        else
            PurchaseDeclined(turretType);
    }

    private void SetPrices() {
        _blueButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = _globalVariables.TurretPrices[TurretTypes.Blue].ToString();
        _greenButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = _globalVariables.TurretPrices[TurretTypes.Green].ToString();
        _yellowButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = _globalVariables.TurretPrices[TurretTypes.Yellow].ToString();
        _redButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = _globalVariables.TurretPrices[TurretTypes.Red].ToString();
    }


    private async void PurchaseAccepted(TurretTypes turretType) {
        _globalVariables.UpdateTurrets(turretType, 1);
        await TransitionManager.Instance.ScaleTransition(iconImages[turretType].transform, Vector3.one, Vector3.one * 0.8f, 0.15f, 0.61f, 0.88f);
        await TransitionManager.Instance.ScaleTransition(iconImages[turretType].transform.transform, Vector3.one * 0.8f, Vector3.one, 0.15f, 0.61f, 0.88f);
    }

    private async void PurchaseDeclined(TurretTypes turretType) {
        iconImages[turretType].color = Color.red;
        await Awaitable.WaitForSecondsAsync(0.3f);
        iconImages[turretType].color = Color.white;
    }


    private void DictSetup() {
        iconImages = new Dictionary<TurretTypes, Image> {
            [TurretTypes.Blue] = _blueIcon,
            [TurretTypes.Green] = _greenIcon,
            [TurretTypes.Yellow] = _yellowIcon,
            [TurretTypes.Red] = _redIcon
        };
    }
}

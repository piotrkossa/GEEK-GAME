using TMPro;
using UnityEngine;

public class Shelved : MonoBehaviour
{

    [SerializeField] private GlobalVariables _globalVariables;
    [SerializeField] TextMeshProUGUI _priceDefault;
    [SerializeField] TextMeshProUGUI _pricePierce;
    [SerializeField] TextMeshProUGUI _priceFire;
    [SerializeField] TextMeshProUGUI _priceFast;

    void Start()
    {
        InputManager.Instance.OnGameObjectClicked.AddListener(CheckIfIconClicked);
    }

    void Update()
    {
        
    }

    private void CheckIfIconClicked(Transform icon)
    {
        if (icon.name == "IconDefault")
        {
            DefaultClicked();
        }

        else if (icon.name == "IconPierce")
        {
            PierceClicked();
        }

        else if (icon.name == "IconFire")
        {
            FireClicked();
        }

        else if (icon.name == "IconFast")
        {
            FastClicked();
        }

    }

    private void DefaultClicked()
    {
        if (int.TryParse(_priceDefault.text, out int priceDefault))
        {
            if (_globalVariables.Money >= priceDefault)
            {
                _globalVariables.UpdateMoney(-priceDefault);
                _globalVariables.UpdateDefault(1);
            }
            else
            {
                Debug.Log("Nie mo¿na zakupiæ dzia³ka");
            }
        }
    }

    private void PierceClicked()
    {
        if (int.TryParse(_pricePierce.text, out int pricePierce))
        {
            if (_globalVariables.Money >= pricePierce)
            {
                _globalVariables.UpdateMoney(-pricePierce);
                _globalVariables.UpdatePierce(1);
            }
            else
            {
                Debug.Log("Nie mo¿na zakupiæ dzia³ka");
            }
        }
    }

    private void FireClicked()
    {
        if (int.TryParse(_priceFire.text, out int priceFire))
        {
            if (_globalVariables.Money >= priceFire)
            {
                _globalVariables.UpdateMoney(-priceFire);
                _globalVariables.UpdateDefault(1);

            }
        }
    }

    private void FastClicked()
    {
        if (int.TryParse(_priceFast.text, out int priceFast))
        {
            if (_globalVariables.Money >= priceFast)
            {
                _globalVariables.UpdateMoney(-priceFast);
                _globalVariables.UpdateFast(1);
            }
            else
            {
                Debug.Log("Nie mo¿na zakupiæ dzia³ka");
            }
        }
    }
}

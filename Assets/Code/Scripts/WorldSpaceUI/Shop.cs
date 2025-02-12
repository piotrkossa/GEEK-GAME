using UnityEngine;

public class Shop : MonoBehaviour
{

    [Header("Settings")]
    [SerializeField] private float shrinkRatio;

    [Header("Images")]
    [SerializeField] private GameObject shopShelves;
    [SerializeField] private GameObject shopImage;

    [Header("External Requirements")]
    [SerializeField] private GlobalVariables _globalVariables;

    private void Start() {
        _globalVariables.onViewChanged.AddListener(CheckIfInShop);
    }

    private bool isItIn = false;

    private void CheckIfInShop() {
        if (_globalVariables.CurrentLevelView == 4) {
            isItIn = true;
            ShopEntered();
        }
        else if (isItIn == true) {
            ShopQuit();
            isItIn = false;
        }
    }


    private async void ShopEntered()
    {
        InputManager.Instance.BlockAllInput();

        await TransitionManager.Instance.ScaleTransition(shopImage.transform, Vector3.one, Vector3.zero, 0.3f, 0.61f, 0.88f);
        shopImage.SetActive(false);

        shopShelves.SetActive(true);
        await TransitionManager.Instance.ScaleTransition(shopShelves.transform, Vector3.zero, Vector3.one, 0.3f, 0.61f, 0.88f);

        InputManager.Instance.UnlockAllInput();
    }

    private async void ShopQuit() {
        InputManager.Instance.BlockAllInput();

        await TransitionManager.Instance.ScaleTransition(shopShelves.transform, Vector3.one, Vector3.zero, 0.3f, 0.61f, 0.88f);
        shopShelves.SetActive(false);

        shopImage.SetActive(true);
        await TransitionManager.Instance.ScaleTransition(shopImage.transform, Vector3.zero, Vector3.one, 0.3f, 0.61f, 0.88f);

        InputManager.Instance.UnlockAllInput();
    }
}

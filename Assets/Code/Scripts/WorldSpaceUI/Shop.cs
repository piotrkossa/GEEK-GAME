using UnityEngine;
using UnityEngine.UIElements;

public class Shop : MonoBehaviour
{

    [Header("Settings")]
    [SerializeField] private float shrinkRatio;

    [Header("Images")]
    [SerializeField] private GameObject shopShelves;
    [SerializeField] private GameObject shopShop;

    private Vector3 originScale;
    private Vector3 clickedScale;

    void Start()
    {
        originScale = shopShop.transform.localScale;
        clickedScale = Vector3.one * shrinkRatio;
        InputManager.Instance.OnGameObjectClicked.AddListener(CheckIfShopClicked);
    }

    private void CheckIfShopClicked(Transform objTransform)
    {
        if (objTransform.name == "ShopImage")
        {
            ShopClicked();
        }
    }

    private bool isTrans = false;
    private async void ShopClicked()
    {
        if (isTrans) return;

        isTrans = true;

        await TransitionManager.Instance.ScaleTransition(shopShelves.transform, originScale, clickedScale, 0.2f, 0.61f, 0.88f);
        await TransitionManager.Instance.ScaleTransition(shopShop.transform, originScale, clickedScale, 0.2f, 0.61f, 0.88f);
        shopShop.SetActive(false);
        shopShelves.SetActive(true);
        await TransitionManager.Instance.ScaleTransition(shopShelves.transform, clickedScale, originScale, 0.2f, 0.61f, 0.88f);


        isTrans = false;
    }
}

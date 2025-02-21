using UnityEngine;
using UnityEngine.UI;

public class Kitten : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float shrinkRatio;

    [Header("External Requirements")]
    [SerializeField] private GlobalVariables _globalVariables;

    [Header("Internal Requirements")]
    [SerializeField] private Image kittenImage;

    [Header("Asset Requirements")]
    [SerializeField] private Sprite loosingCatSprite;
    [SerializeField] private Sprite restingCatSprite;
    [SerializeField] private Sprite clickedCatSprite;

    private Vector3 originScale;
    private Vector3 clickedScale;

    private float maxHealth;

    private bool isAngry = false;

    private void Start() {
        maxHealth = _globalVariables.Health;
        originScale = kittenImage.transform.localScale;
        clickedScale = Vector3.one * shrinkRatio;

        InputManager.Instance.OnGameObjectClicked.AddListener(CheckIfKittenClicked);
        _globalVariables.onUiUpdated.AddListener(CheckIfIsLow);
    }

    private void CheckIfKittenClicked(Transform objTransform) {
        if (objTransform.name == this.name) {
            KittenClicked();
        }
    }

    private bool isTrans = false;

    private async void KittenClicked() {
        if (isTrans) return;

        isTrans = true;

        if (!isAngry)
            kittenImage.sprite = clickedCatSprite;

        await TransitionManager.Instance.ScaleTransition(kittenImage.transform, originScale, clickedScale, 0.2f, 0.61f, 0.88f);
        await TransitionManager.Instance.ScaleTransition(kittenImage.transform, clickedScale, originScale, 0.2f, 0.61f, 0.88f);

        if (!isAngry)
            kittenImage.sprite = restingCatSprite;

        isTrans = false;
    }

    private async void CheckIfIsLow() {
        float hbValue = _globalVariables.Health / maxHealth;
        hbValue = hbValue - (hbValue % 0.125f);

        if (hbValue <= 0.375f) {
            if (isAngry) return;
            isAngry = true;
            await TransitionManager.Instance.ImageColorTransition(kittenImage, new Color(1, 1, 1, 1), new Color(1, 1, 1, 0), 0.4f, 0f, 1f);
            kittenImage.sprite = loosingCatSprite;
            await TransitionManager.Instance.ImageColorTransition(kittenImage, new Color(1, 1, 1, 0), new Color(1, 1, 1, 1), 0.4f, 0f, 1f);
        }
        else {
            if (!isAngry) return;
            await TransitionManager.Instance.ImageColorTransition(kittenImage, new Color(1, 1, 1, 1), new Color(1, 1, 1, 0), 0.4f, 0f, 1f);
            kittenImage.sprite = restingCatSprite;
            await TransitionManager.Instance.ImageColorTransition(kittenImage, new Color(1, 1, 1, 0), new Color(1, 1, 1, 1), 0.4f, 0f, 1f);
            isAngry = false;
        }
    }
}

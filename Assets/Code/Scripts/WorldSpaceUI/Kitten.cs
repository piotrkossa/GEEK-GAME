using UnityEditorInternal;
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

    private void Start() {
        originScale = kittenImage.transform.localScale;
        clickedScale = Vector3.one * shrinkRatio;

        InputManager.Instance.OnGameObjectClicked.AddListener(CheckIfKittenClicked);
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

        kittenImage.sprite = clickedCatSprite;

        await TransitionManager.Instance.ScaleTransition(kittenImage.transform, originScale, clickedScale, 0.2f, 0.61f, 0.88f);
        await TransitionManager.Instance.ScaleTransition(kittenImage.transform, clickedScale, originScale, 0.2f, 0.61f, 0.88f);

        kittenImage.sprite = restingCatSprite;

        isTrans = false;
    }
}

using UnityEngine;
using UnityEngine.UI;
using System.Threading.Tasks;

public class UICostumeChanger : MonoBehaviour
{
    private Image uiImage;

    public Sprite[] costumes;
    public Sprite optionsCostume;

    public float changeInterval = 4f;
    public float fadeDuration = 0.25f;


    private bool isOptionsActive = false;
    private bool isInOptionsNow = false;


    private void Start()
    {
        uiImage = this.GetComponent<Image>();
        ManageCostumes();
    }

    public void EnterOptions()
    {
        isOptionsActive = true;
    }

    public void ExitOptions()
    {
        isOptionsActive = false;
    }

    private async void ManageCostumes() {
        while (true) {
            if (isOptionsActive && !isInOptionsNow) {
                isInOptionsNow = true;
                await TransitionManager.Instance.ImageColorTransition(uiImage, Color.white, new Color(1, 1, 1, 0), fadeDuration, 0f, 1f);
                uiImage.sprite = optionsCostume;
                await TransitionManager.Instance.ImageColorTransition(uiImage, new Color(1, 1, 1, 0), Color.white, fadeDuration, 0f, 1f);
            }
            else if (!isOptionsActive) {
                isInOptionsNow = false;
                await TransitionManager.Instance.ImageColorTransition(uiImage, Color.white, new Color(1, 1, 1, 0), fadeDuration, 0f, 1f);
                uiImage.sprite = costumes[DrawIndex()];
                await TransitionManager.Instance.ImageColorTransition(uiImage, new Color(1, 1, 1, 0), Color.white, fadeDuration, 0f, 1f);
            }
            
            for (int i = 0; i < 31; i++) {
                if (isOptionsActive != isInOptionsNow) break;

                await Awaitable.WaitForSecondsAsync(0.1f);
            }
        }
    }

    
    private int lastIndex;

    private int DrawIndex() {
        int index = Random.Range(0, costumes.Length);
        if (index == lastIndex) {
            if (index == 0) {
                index += 1;
            }
            else {
                index -= 1;
            }
        }

        lastIndex = index;

        return index;
    }
}

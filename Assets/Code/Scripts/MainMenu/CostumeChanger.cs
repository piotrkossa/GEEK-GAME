using UnityEngine;
using UnityEngine.UI;
using System.Threading.Tasks;
using System.Collections;

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
        StartCoroutine(CostumesSetter());
    }

    public void EnterOptions()
    {
        isOptionsActive = true;
    }

    public void ExitOptions()
    {
        isOptionsActive = false;
    }

    private Task task;

    private IEnumerator CostumesSetter() {
        while (true) {
            if (isOptionsActive && !isInOptionsNow) {
                isInOptionsNow = true;
                task = TransitionManager.Instance.ImageColorTransition(uiImage, Color.white, new Color(1, 1, 1, 0), fadeDuration, 0f, 1f);
                yield return new WaitUntil(() => task.IsCompleted);

                uiImage.sprite = optionsCostume;

                task = TransitionManager.Instance.ImageColorTransition(uiImage, new Color(1, 1, 1, 0), Color.white, fadeDuration, 0f, 1f);
                yield return new WaitUntil(() => task.IsCompleted);
            }
            else if (!isOptionsActive) {
                isInOptionsNow = false;
                task = TransitionManager.Instance.ImageColorTransition(uiImage, Color.white, new Color(1, 1, 1, 0), fadeDuration, 0f, 1f);
                yield return new WaitUntil(() => task.IsCompleted);

                uiImage.sprite = costumes[DrawIndex()];
                task =  TransitionManager.Instance.ImageColorTransition(uiImage, new Color(1, 1, 1, 0), Color.white, fadeDuration, 0f, 1f);
                yield return new WaitUntil(() => task.IsCompleted);
            }
            
            for (int i = 0; i < 31; i++) {
                if (isOptionsActive != isInOptionsNow) break;

                yield return new WaitForSeconds(0.1f);
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

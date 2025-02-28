using UnityEngine;
using UnityEngine.UI;
using System.Threading.Tasks;
using System.Collections;

public class Waves : MonoBehaviour
{
    [SerializeField] private Image[] _waves = new Image [3];
    AudioManager audioManager;
    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    public void HideAlerts() {
        foreach (Image image in _waves) {
            audioManager.PlaySFX(audioManager.choice);
            image.gameObject.SetActive(false);
        }
    }

    public IEnumerator ShowWaveInfo(int wave) {
        InputManager.Instance.BlockAllInput();

        this.gameObject.SetActive(true);
        Task task = TransitionManager.Instance.ScaleTransition(_waves[wave].transform, Vector3.zero, Vector3.one, 0.6f, 1f, 1f);
        _waves[wave].gameObject.SetActive(true);

        while (true) {
            if (!_waves[wave].gameObject.activeSelf)
                break;
            yield return null;
        }
        
        InputManager.Instance.UnlockAllInput();
    }
}

using UnityEngine;
using UnityEngine.UI;
using System.Threading.Tasks;

public class Waves : MonoBehaviour
{
    [SerializeField] private Image[] _waves = new Image [3];

    public void HideAlerts() {
        foreach (Image image in _waves) {
            image.gameObject.SetActive(false);
        }
        this.gameObject.SetActive(false);
    }

    public async Task ShowWaveInfo(int wave) {
        InputManager.Instance.BlockAllInput();

        this.gameObject.SetActive(true);
        _waves[wave].gameObject.SetActive(true);

        while (true) {
            await Awaitable.NextFrameAsync();
            if (!this.gameObject.activeSelf)
                break;
        }
        
        InputManager.Instance.UnlockAllInput();
    }
}

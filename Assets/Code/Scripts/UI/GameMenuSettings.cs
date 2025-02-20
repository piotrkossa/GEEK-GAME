using UnityEngine;
using UnityEngine.UI;

public class GameMenuSettings : MonoBehaviour
{

    [SerializeField] GameObject settingScreen;
    [SerializeField] GameObject buttonsScreen;
    AudioManager audioManager;
    public Slider musicSlider;
    public Slider sfxSlider;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    private void Start()
    {
        musicSlider.value = PlayerPrefs.GetFloat("MusicSliderValue", 1f);
        sfxSlider.value = PlayerPrefs.GetFloat("SFXSliderValue", 1f);

        musicSlider.onValueChanged.AddListener((value) =>
        {
            PlayerPrefs.SetFloat("MusicSliderValue", value);
            PlayerPrefs.Save();
        });

        sfxSlider.onValueChanged.AddListener((value) =>
        {
            PlayerPrefs.SetFloat("SFXSliderValue", value);
            PlayerPrefs.Save();
        });
    }

    public void musicVolume(float volume)
    {
        audioManager.SetMusicVolume(volume);
    }

    public void sfxVolume(float volume)
    {
        audioManager.SetSFXVolume(volume);
    }

    public void SetFullscreen (bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }

    public void Back()
    {
        settingScreen.SetActive(false);
        buttonsScreen.SetActive(true);
    }
}

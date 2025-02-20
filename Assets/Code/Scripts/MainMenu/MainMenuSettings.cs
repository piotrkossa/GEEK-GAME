using UnityEngine;

public class MainMenuSettings : MonoBehaviour
{
    AudioManager audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
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
}

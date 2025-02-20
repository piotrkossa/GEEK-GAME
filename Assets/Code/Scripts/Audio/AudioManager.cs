using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    [Header("Audio Source")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;

    [Header("Audio Clip")]
    public AudioClip buy;
    public AudioClip shoot;
    public AudioClip mainMenu;
    public AudioClip background;
    public AudioClip gameover;
    public AudioClip choice;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        SetMusicVolume(PlayerPrefs.GetFloat("MusicSliderValue", 1f));
        SetSFXVolume(PlayerPrefs.GetFloat("SFXSliderValue", 1f));
    }

    private void Start()
    {
        musicSource.loop = true;
        musicSource.clip = mainMenu;
        musicSource.Play();
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(UnityEngine.SceneManagement.Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "MainScene")
        {
            PlayMainSceneMusic();
        }
    }

    private void PlayMainSceneMusic()
    {
        if (musicSource.clip != background)
        {
            musicSource.clip = background;
            musicSource.Play();
        }
    }

    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }

    public void SetMusicVolume(float volume)
    {
        musicSource.volume = volume;
    }

    public void SetSFXVolume(float volume)
    {
        SFXSource.volume = volume;
    }
}

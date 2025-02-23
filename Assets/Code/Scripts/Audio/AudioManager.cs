using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    [Header("Audio Source")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;

    [Header("Audio Clip Menu")]
    public AudioClip mainMenu;
    public AudioClip choice;
    public AudioClip backlight;

    [Header ("Audio Clip Cutscene")]
    public AudioClip cutscene;

    [Header("Audio Clip In Game")]
    public AudioClip wave1;
    public AudioClip wave2;
    public AudioClip wave3;

    public AudioClip buyAccepted;
    public AudioClip buyDeclined;
    public AudioClip shoot;
    public AudioClip dead;
    public AudioClip kittenMeow;
    public AudioClip placing;
    public AudioClip question;

    [Header("Audio Clip Gameover")]
    public AudioClip gameover;


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
        if (scene.name == "Cutscene")
        {
            PlayCutSceneMusic();
        }

        else if (scene.name == "MainScene")
        {
            PlayMainSceneMusic();
        }

        else if (scene.name == "GameOver")
        {
            PlayEndSceneMusic();
        }
    }

    private void PlayMainSceneMusic()
    {
        if (musicSource.clip != wave1)
        {
            musicSource.Stop();
            musicSource.clip = wave1;
            musicSource.Play();
        }
    }

    public void PlayWaveMusic(int wave)
    {
        musicSource.Stop();
        switch (wave)
        {
            case 1:
                musicSource.clip = wave2;
                break;
            case 2:
                musicSource.clip = wave3;
                break;
            default:
                Debug.LogWarning("Nieznany numer fali: " + wave);
                return;
        }
        musicSource.Play();
    }

    private void PlayEndSceneMusic()
    {
        if (musicSource.clip != gameover)
        {
            musicSource.Stop();
            musicSource.clip = gameover;
            musicSource.Play();
        }
    }

    private void PlayCutSceneMusic()
    {
        if (musicSource.clip != cutscene)
        {
            musicSource.Stop();
            musicSource.clip = cutscene;
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

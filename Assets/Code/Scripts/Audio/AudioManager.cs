using System.Collections;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("Audio Source")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;

    [Header("Audio Clip")]
    public AudioClip buy;
    public AudioClip shoot;
    public AudioClip[] soundtracks;
    public AudioClip gameover;

    private int loopsPerClip = 3;

    private void Start()
    {
        musicSource.loop = false;
        StartCoroutine(PlaySoundtrack());
    }

    IEnumerator PlaySoundtrack()
    {
        for (int i = 0; i < soundtracks.Length; i++)
        {
            for (int loop = 0; loop < loopsPerClip; loop++)
            {
                musicSource.clip = soundtracks[i];
                musicSource.Play();

                yield return new WaitForSeconds(soundtracks[i].length);
            }
        }
    }

    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }

}

using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    [SerializeField] private AudioClip[] musicClips;
    [SerializeField] private AudioSource musicSource;

    [SerializeField] private AudioSource[] soundFXs;
    [SerializeField] private AudioSource[] vocalClips;

    private AudioClip randomClip;

    public bool playMusic = true;
    public bool playFX = true;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        randomClip = RandomClip(musicClips);
        PlayBackgroundMusic(randomClip);
    }

    AudioClip RandomClip(AudioClip[] clips) => clips[Random.Range(0, clips.Length)];

    void UpdateMusic()
    {
        if (musicSource.isPlaying != playMusic)
        {
            if (playMusic)
            {
                randomClip = RandomClip(musicClips);
                PlayBackgroundMusic(randomClip);
            }
            else
            {
                musicSource.Stop();
            }
        }
    }

    public void PlayVocal()
    {
        if (playFX)
        {
            AudioSource src = vocalClips[Random.Range(0, vocalClips.Length)];
            src.Stop();
            src.Play();
        }
    }

    public void PlayFX(int fx)
    {
        if (playFX && fx < soundFXs.Length)
        {
            soundFXs[fx].Stop();
            soundFXs[fx].Play();
        }
    }

    public void PlayBackgroundMusic(AudioClip clip)
    {
        if (!clip || !musicSource || !playMusic) return;

        musicSource.clip = clip;
        musicSource.Play();
    }

    public void ToggleMusic()
    {
        playMusic = !playMusic;
        UpdateMusic();
    }

    public void ToggleFX()
    {
        playFX = !playFX;
    }
}
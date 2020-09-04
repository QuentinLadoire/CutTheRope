using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public enum MusicType
{
    menu,
    game,
}
public enum SoundType
{
    Tap,
    Win,
    Star1,
    Star2,
    Star3,
    Bubble,
    BubbleBreak,
    CandyLink,
    RopeBleak1,
    RopeBleak2,
    RopeBleak3,
    RopeBleak4,
    CandyBreak,
    MonsterSad,
}

public class SoundManager : MonoBehaviour {

    public static SoundManager instance = null;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

        CreateAudioSource();
    }

    [SerializeField] AudioClip[] musicClip;
    [SerializeField] AudioClip[] soundClip;

    [SerializeField] int nbSource = 5;
    AudioSource musicSource;
    List<AudioSource> soundSource = new List<AudioSource>();

    public bool activeSound = true;
    public bool activeMusic = true;


    void CreateAudioSource()
    {
        musicSource = gameObject.AddComponent<AudioSource>();
        for (int i = 0; i < nbSource; i++)
        {
            soundSource.Add(gameObject.AddComponent<AudioSource>());
        }
    }
    public void PlayMusic(MusicType musicType)
    {
        musicSource.Stop();
        musicSource.clip = musicClip[(int)musicType];
        musicSource.Play();
    }
    public void PlaySound(SoundType soundType)
    {
        for (int i = 0; i < soundSource.Count; i++)
        {
            if (!soundSource[i].isPlaying)
            {
                soundSource[i].clip = soundClip[(int)soundType];
                soundSource[i].Play();
                return;
            }
        }
    }
    public bool IsPlaying(MusicType musicType)
    {
        return (musicSource.clip == musicClip[(int)musicType] && musicSource.isPlaying);
    }

    public void ActiveMusic()
    {
        musicSource.mute = !musicSource.mute;
    }
    public void ActiveSound()
    {
        for (int i = 0; i < nbSource; i++)
        {
            soundSource[i].mute = !soundSource[i].mute;
        }
    }
}

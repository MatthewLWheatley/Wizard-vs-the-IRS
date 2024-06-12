using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MusicPlayer : MonoBehaviour
{
    public AudioSource m_MusicSource;

    private static MusicPlayer instance;

    public static MusicPlayer Instance
    {
        get
        {
            if (instance == null)
            {
                Debug.LogError("Manager is null");
            }
            return instance;
        }
    }

    private void Awake()
    {
        instance = this;
        m_MusicSource = GetComponent<AudioSource>();
    }

    public void PlayMusic(AudioClip music)
    {
        // Dont play sound if alreafy being played
        if (music != m_MusicSource.clip)
        {
            m_MusicSource.clip = music;
            m_MusicSource.Play();
        }
    }

    public void ChangeLoop(bool isLoop)
    {
        m_MusicSource.loop = isLoop;
    }
}

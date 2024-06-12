using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayMusic : MonoBehaviour
{
    // Plays level music on start

    [SerializeField] private AudioClip m_Music;
    private void Awake()
    {
        MusicPlayer.Instance.PlayMusic(m_Music);
    }
}
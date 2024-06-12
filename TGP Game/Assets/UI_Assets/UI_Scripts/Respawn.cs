using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    private LevelManager m_LevelManager;
    [SerializeField] private AudioClip m_AudioClip;
    // Start is called before the first frame update
    void Start()
    {
        m_LevelManager = GameObject.FindObjectOfType<LevelManager>();
    }

    public void RespawnCall()
    {
        MusicPlayer.Instance.PlayMusic(m_AudioClip);
        MusicPlayer.Instance.ChangeLoop(true);


        m_LevelManager.RespawnLevel();
    }
}

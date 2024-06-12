using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class DeathScreenAnimation : MonoBehaviour
{
    [SerializeField] private float m_duration;
    [SerializeField] private float m_delay;
    [SerializeField] private Camera m_DeathCamera;
    [SerializeField] private AudioClip m_Clip;
    private GameObject m_Player;
    private void Start()
    {
        MusicPlayer.Instance.ChangeLoop(false);
        MusicPlayer.Instance.PlayMusic(m_Clip);
        //SceneManager.UnloadScene(0);
        StartCoroutine(C_DeathScale());
    }
    IEnumerator C_DeathScale()
    {
        m_DeathCamera.gameObject.SetActive(true);
        yield return new WaitForSeconds(m_delay);
        transform.DOScale(Vector3.one, m_duration);
    }
}

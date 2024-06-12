using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Rendering.Universal;

public class DeathIcon : MonoBehaviour
{
    public Vector3 m_StartPosition;
    [SerializeField] private Vector2 m_MoveDestination;
    [SerializeField] private float m_duration;
    [SerializeField] private Color m_StartColor;
    [SerializeField] private Color m_EndColor;
    [SerializeField] private Vector3 m_StartScale;
    [SerializeField] private Vector3 m_EndScale;
    [SerializeField] private Vector3 m_Rotation;
    [SerializeField] private AudioClip m_HealthPickupSound;
    private Camera m_Camera;
    private void Start()
    {
        transform.position = m_StartPosition;
        transform.position.Scale(m_StartScale);
        m_Camera = Camera.main;
        transform.parent.DORotate(m_Rotation, 0);
        //transform.DOLocalMove(m_MoveDestination, m_duration);
        transform.DOShakeScale(m_duration);
        StartCoroutine(C_Destroy());
    }

    IEnumerator C_Destroy()
    {
        yield return new WaitForSeconds(m_duration);
        Destroy(transform.parent.gameObject);
    }
}


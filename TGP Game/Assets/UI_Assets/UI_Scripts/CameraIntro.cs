using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class CameraIntro : MonoBehaviour
{
    [SerializeField]private float m_duration;
    [SerializeField]private float m_ZoomDuration;
    [SerializeField] private Vector3[] m_Waypoints;
    [SerializeField] private GameObject m_LookAt;
    [SerializeField] private Vector3[] m_WaypointsZoom;
    [SerializeField] private GameObject m_LookAtZoom;
    private Vector3 m_ReturnPosition;
    private Tween m_Intro;
    void Start()
    {
        Time.timeScale = 1;
        m_Intro = gameObject.transform.DOPath(m_Waypoints, m_duration).SetLookAt(m_LookAt.transform, true);
    }
    public void CameraZoom()
    {
        m_Intro.Pause();
        m_ReturnPosition = gameObject.transform.position;
        gameObject.transform.DOPath(m_WaypointsZoom, m_ZoomDuration);
        gameObject.transform.DODynamicLookAt(m_LookAtZoom.transform.position,m_ZoomDuration);
    }
    public void CameraZoomOut()
    {
        gameObject.transform.DOPath(m_Waypoints, m_ZoomDuration);
        gameObject.transform.DODynamicLookAt(m_LookAt.transform.position, m_ZoomDuration);
    }
}


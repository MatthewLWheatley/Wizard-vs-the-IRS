using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ScaleUp : MonoBehaviour
{
    [SerializeField] private float m_ScaleDuration;
    [SerializeField] private float m_Duration;
    [SerializeField]private float m_Strenth;
    [SerializeField] GameObject[] m_InformationBoxes;
    [SerializeField] private Pausing m_PauseSetting;
    private Vector3 m_ScreenCentre;
    private bool isOpen = false;
    private void Start()
    {
        //ISSUE: if the window is resized during gameplay then screen centre doesn't change meaning the inventory is off centre.
        m_ScreenCentre = transform.position;
        transform.position = Vector3.zero;
        transform.localScale = Vector3.zero;
    }
    private void Update()
    {
        m_ScreenCentre.x = Screen.width / 2;
        m_ScreenCentre.y = Screen.height / 2;
    }
    public void InventoryOpening()
    {
        StartCoroutine(C_ScalingUp());
    }
    public void InventoryClosing()
    {
        StartCoroutine(C_ScalingDown());
    }
    IEnumerator C_ScalingUp()
    {
        if (!isOpen)
        {
            Tween MoveTween = transform.DOMove(m_ScreenCentre, 1);
            Tween ScaleTween = transform.DOScale(Vector3.one, m_ScaleDuration);
            yield return ScaleTween.WaitForCompletion();
            Tween TransformTween = transform.DOShakeScale(m_Duration, m_Strenth);
            yield return TransformTween.WaitForCompletion();
            isOpen = true;
            m_PauseSetting.Pause();

        }

    }
    IEnumerator C_ScalingDown()
    {
        if (isOpen)
        {
            m_PauseSetting.UnPause();
            for (int i = 0; i < m_InformationBoxes.Length; i++)
            {
                m_InformationBoxes[i].SetActive(false);
            }
            Tween MoveTween = transform.DOMove(Vector3.zero, 1);
            Tween ScaleTween = transform.DOScale(Vector3.zero, m_ScaleDuration);
            yield return ScaleTween.WaitForCompletion();
            transform.DOShakeScale(m_Duration, m_Strenth);
            isOpen = false;
        }

    }
}

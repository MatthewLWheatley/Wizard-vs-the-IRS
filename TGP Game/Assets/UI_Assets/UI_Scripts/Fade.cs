using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class Fade : MonoBehaviour
{
    [SerializeField][Range(0,1)] private float m_ValueTo;
    [SerializeField][Range(0, 1)] private float m_ValueFrom;
    [SerializeField][Min(0f)] private float m_Duration;
    [SerializeField][Min(0f)] private float m_InitialDelay;
    public void FadingVignette()
    {
        StartCoroutine(C_FadingUI());
    }
    IEnumerator C_FadingUI()
    {
        yield return new WaitForSeconds(m_InitialDelay);
        Tween FadeIn = GetComponentInChildren<Image>().DOFade(m_ValueTo, m_Duration);
        yield return FadeIn.WaitForCompletion();
        GetComponentInChildren<Image>().DOFade(m_ValueFrom, m_Duration);
    }
}

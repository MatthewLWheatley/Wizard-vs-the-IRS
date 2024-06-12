using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DeathUIFading : MonoBehaviour
{
    [SerializeField][Range(0, 1)] private float m_ValueTo;
    [SerializeField][Min(0f)] private float m_Duration;
    [SerializeField][Min(0f)] private float m_InitialDelay;
    // Start is called before the first frame update
    private void Start()
    {
        StartCoroutine(C_FadingDeathMenu());
    }

    IEnumerator C_FadingDeathMenu()
    {
        yield return new WaitForSeconds(m_InitialDelay);
        GetComponent<CanvasGroup>().DOFade(m_ValueTo, m_Duration);
    }
}

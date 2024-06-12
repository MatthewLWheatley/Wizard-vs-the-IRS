using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class GlowPulse : MonoBehaviour
{
    bool m_InArea = true;
    public void DoGlowPulse()
    {
        m_InArea = true;
        StartCoroutine(C_GlowPulseLoop());
    }
    public void ExitArea()
    {
        m_InArea=false;

    }
    IEnumerator C_GlowPulseLoop()
    {
        while (m_InArea)
        {
            Tween FadeOutTween = this.GetComponent<Image>().DOFade(0, 1.5f);
            yield return FadeOutTween.WaitForCompletion();
            Tween FadeInTween = this.GetComponent<Image>().DOFade(0.75f, 1.5f);
            yield return FadeInTween.WaitForCompletion();
        }
        var TempColor = GetComponent<Image>().color;
        TempColor.a = 0f;
        GetComponent<Image>().color = TempColor;
    }
}

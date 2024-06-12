using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class LoadingJump : MonoBehaviour
{
    [SerializeField] private Image[] Dots;
    [SerializeField] private float m_JumpPower = 10;
    [SerializeField] private float m_JumpDuration = 1;
    private void Start()
    {
        if(this.gameObject.activeInHierarchy == true)
        {
            StartCoroutine(C_JumpDots());
        }
    }
     IEnumerator C_JumpDots()
    {
        for (int i = 0; i < Dots.Length; i++)
        {
            Tween JumpingDot = Dots[i].transform.DOJump(Dots[i].transform.position, m_JumpPower, 1, m_JumpDuration);
            yield return JumpingDot.WaitForCompletion();
            if(i== Dots.Length-1)
            {
                i = -1;
            }
        }

    }
}

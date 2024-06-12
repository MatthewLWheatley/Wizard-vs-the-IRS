using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PageTurn : MonoBehaviour
{
    [SerializeField][Min(0f)] private float m_Duration;
    private Quaternion m_InitialRotation;

    private void Start()
    {
        //Takes the initial rotation to use when reseting the pages position after the animation is complete.
        m_InitialRotation = gameObject.transform.localRotation;
        StartCoroutine(C_PageTurning());

    }
    public void Turning()
    {
        StartCoroutine(C_PageTurnOnClick());
    }
    public void TurningBack()
    {
        StartCoroutine(C_PageTurning());
    }
    IEnumerator C_PageTurning()
    {
        Tween PageTurnTween = gameObject.transform.DOLocalRotate(new Vector3(gameObject.transform.position.x, -180, gameObject.transform.position.z), m_Duration);
        yield return PageTurnTween.WaitForCompletion();
        //gameObject.transform.localRotation = m_InitialRotation;
    }

    IEnumerator C_PageTurnOnClick()
    {
        Tween PageTurnTween = gameObject.transform.DOLocalRotate(new Vector3(m_InitialRotation.x, m_InitialRotation.y-0.01f, m_InitialRotation.z), m_Duration);
        yield return PageTurnTween.WaitForCompletion();
    }
}

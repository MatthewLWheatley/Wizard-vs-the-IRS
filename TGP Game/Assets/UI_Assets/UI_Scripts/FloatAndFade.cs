using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
public class FloatAndFade : MonoBehaviour
{
    [SerializeField] private Vector3 m_MoveDestination;
    [SerializeField][Tooltip("Length of time before gameobject is destroyed")] private float m_duration;
    [SerializeField] private Color m_StartColor;
    [SerializeField] private Color m_EndColor;
    [SerializeField] private Color m_StartColorCritical;
    [SerializeField] private Color m_EndColorCritical;
    [SerializeField] private Vector3 m_StartScale;
    [SerializeField] private Vector3 m_EndScale;
    [SerializeField] private Vector3 m_Rotation;

    private Camera m_Camera;
    public bool m_IsCritical;
    public string m_DamageDoneText = "99";
    private void Start()
    {
        transform.position.Scale(m_StartScale);
        m_Camera = Camera.main;
        TextMeshProUGUI TextVariable = GetComponent<TextMeshProUGUI>();
        TextVariable.text = m_DamageDoneText;
        
        //if statement handles critical hits and applies different changes for each
        if (!m_IsCritical)
        {
            TextVariable.color = m_StartColor;
            TextVariable.DOColor(m_EndColor, m_duration);
        }
        else
        {
            TextVariable.color = m_StartColorCritical;
            TextVariable.DOColor(m_EndColorCritical, m_duration);
            TextVariable.fontStyle = FontStyles.Bold;
        }

        //All code here moves and modifies the text
        transform.parent.DORotate(m_Rotation,0);
        transform.DOLocalMove(m_MoveDestination, m_duration);
        transform.DOScale(m_EndScale,m_duration);
        TextVariable.DOFade(0,m_duration);
        StartCoroutine(C_Destroy());
    }
    IEnumerator C_Destroy()
    {
        yield return new WaitForSeconds(m_duration);
        Destroy(transform.parent.gameObject);
    }
}

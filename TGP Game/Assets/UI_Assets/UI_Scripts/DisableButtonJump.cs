using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;
using UnityEngine.EventSystems;

public class DisableButtonJump : MonoBehaviour
{
    [Tooltip("After this amount of times the buttons become active")]
    [SerializeField] private float m_DelayTime;
    [SerializeField] private EventTrigger[] m_ButtonJumpList;


    //This script is to prevent buttons jumping while the page is turning as this causes errors
    private void Start()
    {
        for (int i = 0; i < m_ButtonJumpList.Length; i++)
        {
            m_ButtonJumpList[i].enabled = false;
        }
    }
    public void EnableButtons()
    {
        StartCoroutine(C_ButtonEnableCountdown());
    }
    IEnumerator C_ButtonEnableCountdown()
    {
        yield return new WaitForSeconds(m_DelayTime);
        for(int i = 0; i < m_ButtonJumpList.Length; i++)
        {
            m_ButtonJumpList[i].enabled = true;
        }
    }
    public void DisableButtons()
    {
        StartCoroutine(C_ButtonsDisable());
    }
    IEnumerator C_ButtonsDisable()
    {
        for (int i = 0; i < m_ButtonJumpList.Length; i++)
        {
            m_ButtonJumpList[i].enabled = false;
        }
        yield return null;
    }
}

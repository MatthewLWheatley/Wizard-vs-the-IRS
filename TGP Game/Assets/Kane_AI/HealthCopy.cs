using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
public class HealthCopy : MonoBehaviour
{
    private float m_Scale=1;
    private float m_Timer;
    private Image m_Image;
    [SerializeField] private float m_Frequency;
    [SerializeField] private float m_HealthRemove;
    private void Start()
    {
        m_Image=GetComponent<Image>();
    }
    //Not Linked currently, however we can add in a variable here to take away health from the player so it matches the healthbar.
    private void Update()
    {
        m_Timer += Time.deltaTime;
        if (m_Timer > m_Frequency)
        {
            m_Scale -= m_HealthRemove;
            if (m_Scale < 0)
            {
                m_Scale = 0;
            }
            this.transform.localScale = new Vector3(m_Scale,1,1);
            m_Timer = 0;
        }
        //  Changing colour of health bar based on amount of health left
        if (m_Scale <= 1 && m_Scale > 0.5)
        {
            m_Image.DOColor(Color.green, 1);
        }
        if (m_Scale <= 0.5 && m_Scale > 0.2)
        {
            m_Image.DOColor(Color.yellow, 1);
        }
        if (m_Scale <= 0.2)
        {
            m_Image.DOColor(Color.red, 1);
        }

        if (Input.GetKeyDown(KeyCode.H))
        {
            m_Scale = 1;
        }

    }
    public void  Replenish(float value)
    {
        m_Scale += value;
    }
    public void Damaged(float value)
    {
        m_Scale -= value;
    }
}

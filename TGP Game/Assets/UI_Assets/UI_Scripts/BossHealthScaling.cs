using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
public class BossHealthScaling : MonoBehaviour
{
    private float m_Scale = 1;
    private Image m_Image;
    public float m_Health = 100;
    public float m_MaxHealth = 100;
    [Tooltip("If the health bar is below this level the colour changes to yellow")]
    [SerializeField] private float m_YellowColourLevel;
    [Tooltip("If the health bar is below this level the colour changes to red")]
    [SerializeField] private float m_RedColourLevel;
    [SerializeField] private GameObject m_BossDefeatDisplay;
    private void Start()
    {
        m_Image = GetComponent<Image>();
    }
    public void BossDeathUI()
    {
        m_BossDefeatDisplay.SetActive(true);
    }
    private void FixedUpdate()
    {
        m_Scale = m_Health / m_MaxHealth;
        transform.localScale = new Vector3(m_Scale, 1, 1);

        //  Changing colour of health bar based on amount of health left
        if (m_Scale <= 1 && m_Scale > m_YellowColourLevel)
        {
            m_Image.DOColor(Color.green, 1);
        }
        if (m_Scale <= m_YellowColourLevel && m_Scale > m_RedColourLevel)
        {
            m_Image.DOColor(Color.yellow, 1);
        }
        if (m_Scale <= m_RedColourLevel)
        {
            m_Image.DOColor(Color.red, 1);
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
public class HealthBarScaling : MonoBehaviour
{
    private float m_Scale = 1;
    private float m_Timer;
    private Image m_Image;
    [Tooltip("The frequency at which the health bar decreases")]
    [SerializeField] private float m_Frequency;
    [Tooltip("The amount at which the health bar decreases")]
    [SerializeField] private float m_HealthRemove;
    private float m_health = Health.CurrentHealth;
    [Tooltip("If the health bar is below this level the colour changes to yellow")]
    [SerializeField] private float m_YellowColourLevel;
    [Tooltip("If the health bar is below this level the colour changes to red")]
    [SerializeField] private float m_RedColourLevel;
    private void Start()
    {
        m_Image = GetComponent<Image>();
    }
    //Not Linked currently, however we can add in a variable here to take away health from the player so it matches the healthbar.
    private void Update()
    {
        m_Timer += Time.deltaTime;

        m_health = Health.CurrentHealth;
        m_Scale = m_health / 100;
        this.transform.localScale = new Vector3(m_Scale, 1, 1);

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

        if (Input.GetKeyDown(KeyCode.H))
        {
            m_Scale = 1;
        }

    }
}
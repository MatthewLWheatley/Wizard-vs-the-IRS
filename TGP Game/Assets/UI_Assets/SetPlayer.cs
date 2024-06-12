using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetPlayer : MonoBehaviour
{
    [SerializeField] private GameObject m_DeathMenu;
    [SerializeField] private GameObject m_HUD;
    static public GameObject m_PlayerRef;
    public void GetPlayer() 
    {
        m_PlayerRef = GameObject.FindGameObjectWithTag("Player");
    }
    public void FixedUpdate()
    {
        if (m_PlayerRef != null)
        {
            if (m_PlayerRef.GetComponent<Health>().Death)
            {
                m_DeathMenu.SetActive(true);
                m_HUD.SetActive(false);
            }
        }

    }
}

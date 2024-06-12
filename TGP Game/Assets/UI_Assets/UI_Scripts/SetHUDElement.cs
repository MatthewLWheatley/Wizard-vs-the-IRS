using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetHUDElement : MonoBehaviour
{
    static public GameObject m_HudPickup;
    // Start is called before the first frame update
    void Start()
    {
        m_HudPickup = GameObject.FindGameObjectWithTag("HUD");
        m_HudPickup.SetActive(false);
    }
}

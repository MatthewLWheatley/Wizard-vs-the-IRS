using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    [SerializeField] private ScaleUp m_Inventory;
    public float m_Health=100;
    private bool m_InventoryOpenState = false;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (!m_InventoryOpenState)
            {
                m_Inventory.InventoryOpening();
                m_InventoryOpenState = true;
            }
            else 
            { 
                m_Inventory.InventoryClosing();
                m_InventoryOpenState = false;
            }
        }
    }
}

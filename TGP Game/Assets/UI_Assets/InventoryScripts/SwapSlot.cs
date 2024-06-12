using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwapSlot : MonoBehaviour
{
    [SerializeField] private WeaponDatabase m_WeaponDatabase;
    [SerializeField] private Inventory m_Inventory;
    private int m_InvSlot1;
    private int m_InvSlot2;
    public void SwitchingSlots()
    {
        m_InvSlot1 = m_Inventory.inventoryList[0];
        m_InvSlot2 = m_Inventory.inventoryList[1];
        m_Inventory.inventoryList[0] = 99;
        m_Inventory.inventoryList[1] = 99;
        m_Inventory.SetItemID(m_InvSlot1);
        m_Inventory.SlotSelection(1); 
        m_Inventory.SetItemID(m_InvSlot2);
        m_Inventory.SlotSelection(0);

    }
    //Create Temp Variables for weapons
    //assign left mouse to the old right item
    //ditto for the inverse
    //Set images to be accurate

}

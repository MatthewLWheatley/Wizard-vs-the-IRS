using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickUpObject : MonoBehaviour
{
    public int ID;
    [SerializeField] private Inventory m_Inv;
    public GameObject m_WeaponEquipMenu;
    //Add to this script an on trigger enter, this will trigger the WeaponSwitch menu to become active

    private void Start()
    {
        //Change this as searching like this is inneficient?
        m_WeaponEquipMenu = SetHUDElement.m_HudPickup;
        m_Inv = FindObjectOfType<Inventory>();
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
                m_Inv.SetItemID(ID,gameObject);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            m_WeaponEquipMenu.SetActive(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            m_WeaponEquipMenu.SetActive(false);
            
        }
    }
}

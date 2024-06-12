using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryItemText : MonoBehaviour
{
    [SerializeField] private Image m_InventoryImage;
    [SerializeField] private WeaponDatabase m_WeaponDatabase;
    [SerializeField] private TextMeshProUGUI m_Title;
    [SerializeField] private TextMeshProUGUI m_Description;


    public void SetText()
    {
        foreach (var item in m_WeaponDatabase.InventoryDatabase)
        {
            if (item.m_WeaponSprite == m_InventoryImage.sprite)
            {
                m_Title.text = item.m_WeaponName;
                m_Description.text = item.m_WeaponDescription;
            }
            
        }
    }
}

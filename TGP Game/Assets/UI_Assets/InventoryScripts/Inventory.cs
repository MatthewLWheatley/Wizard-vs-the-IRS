using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public int[] inventoryList = new int[1];
    [SerializeField]WeaponDatabase m_WeaponDatabase;
    [SerializeField] Image[] InventorySlots;
    [SerializeField] Image[] InventoryTypeIndicator;

    public int ItemPickedUpID;
    public Weapon_Base[] Items;
    [SerializeField] private GameObject m_PlayerCharacter;
    [SerializeField] private Image m_PickupSlotImage;
    [SerializeField] private Image m_PickupSlotTypeImage;

    //Enlarged Inventory Images
    [SerializeField] private Image m_OpenInventory0;
    [SerializeField] private Image m_OpenInventory1;
    [SerializeField] private Sprite m_NullReference;

    [SerializeField] private Sprite[] m_TypeIndicatorSprites;
    [SerializeField] private GameObject m_HudPickupElement;
    private GameObject m_DeletePickup;
    private void Start()
    {
        //Gets each weapon from the player character and assings them to Items

        //Items = m_PlayerCharacter.GetComponents<Weapon_Base>();
    }
    public void SetPlayer()
    {
        m_PlayerCharacter = GameObject.FindGameObjectWithTag("Player") ;
        Items = m_PlayerCharacter.GetComponents<Weapon_Base>();
    }
    public void SetItemID(int ID, GameObject Obj = null)
    {
        //Assigns the ItemPickedUpID variable to the index of the item on the ground and updates the sprite that shows the item on the ground
        if(Obj != null)
        {
            m_DeletePickup = Obj;
        }
        ItemPickedUpID = ID;
        if (ItemPickedUpID != 99)
        {
            m_PickupSlotImage.sprite = m_WeaponDatabase.InventoryDatabase[ItemPickedUpID].m_WeaponSprite;
            m_PickupSlotTypeImage.sprite = m_TypeIndicatorSprites[m_WeaponDatabase.InventoryDatabase[ItemPickedUpID].WeaponType];
        }
    }
    public void SlotSelection(int ButtonNumber)
    {
        bool Unassigned = true;
        //Checks to see if either of the inventory slots is already equal to the pickup weapons ID
        //If its already present in InventorySlots it can't be added again

        for(int i = 0; i < InventorySlots.Length; i++)
        {
            if (inventoryList[i] == ItemPickedUpID)
            {
                Unassigned = false;
            }
        }
        if (Unassigned)
        {
            m_HudPickupElement.gameObject.SetActive(false);
            Destroy(m_DeletePickup);
            SetMouseButton(ButtonNumber);
            inventoryList[ButtonNumber] = ItemPickedUpID;
            InventorySlots[ButtonNumber].sprite = m_WeaponDatabase.InventoryDatabase[ItemPickedUpID].m_WeaponSprite;
            InventoryTypeIndicator[ButtonNumber].sprite = m_TypeIndicatorSprites[m_WeaponDatabase.InventoryDatabase[ItemPickedUpID].WeaponType];
            //Update the Images in the expanded inventory

            m_OpenInventory0.sprite = InventorySlots[0].sprite;
            m_OpenInventory1.sprite = InventorySlots[1].sprite;
        }


    }
    public void SetMouseButton(int ButtonNumber)
    {
        //Searches the list of weapons until a weapon with the same ID as the currently being picked up weapon

        for (int i = 0; i < Items.Length; i++)
        {
            if (Items[i].ID == ItemPickedUpID)
            {
                if(ButtonNumber == 0)
                {
                    //Unasigns any weapon that was previously set to mouse 0

                    for(int j = 0; j < Items.Length; j++)
                        if(Items[j].fire == 0)
                        {
                            Items[j].fire = 99;
                            Items[j].enabled = false;
                        }

                    //sets the desired weapons fire button to mouse 0
                    Items[i].enabled = true;
                    Items[i].fire = 0;
                }
                else
                {
                    //Unasigns any weapon that was previously set to mouse 1

                    for (int j = 0; j < Items.Length; j++)
                        if (Items[j].fire == 1)
                        {
                            Items[j].fire = 99;
                            Items[j].enabled = false;
                        }

                    //sets the desired weapons fire button to mouse 1
                    Items[i].enabled = true;
                    Items[i].fire = 1;
                }
            }
        }
    }
}

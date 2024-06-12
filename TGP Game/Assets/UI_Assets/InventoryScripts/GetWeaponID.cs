using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetWeaponID : MonoBehaviour
{
    //This list contains all the weapon scripts
    [SerializeField] private Weapon_Base[] Items;
    [SerializeField] private GameObject PlayerCharacter;

    private void Start()
    {
        Items = PlayerCharacter.GetComponents<Weapon_Base>();
    }
    public int SetMouseButton(int searchID)
    {
        //Here I would increment through a list searching for an item with IDNum matching the one of the picked up item
        for(int i = 0; i < Items.Length; i++)
        {
            if (Items[i].ID == searchID)
            {
                //Once finding the matching item we can assign its firing button
                Items[i].fire = 1;
                return 0;
                //assign  fire button to weapon 0
            }
        }
        return 99;


    }
    //Assign the fire button
}

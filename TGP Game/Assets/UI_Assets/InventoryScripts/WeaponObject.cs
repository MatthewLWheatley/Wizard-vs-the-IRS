using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponObject
{
    public string m_WeaponName;
    public string m_WeaponDescription;
    public Sprite m_WeaponSprite;
    public int WeaponType;

    public WeaponObject(string WeaponName, Sprite WeaponSprite, string m_WeaponDescription, int WeaponType = 0)
    {
        this.m_WeaponName = WeaponName;
        this.m_WeaponSprite = WeaponSprite;
        this.WeaponType = WeaponType;
        this.m_WeaponDescription = m_WeaponDescription;
    }
}

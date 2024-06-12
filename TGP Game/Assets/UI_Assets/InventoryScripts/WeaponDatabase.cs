using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponDatabase : MonoBehaviour
{
    public List<WeaponObject> InventoryDatabase = new List<WeaponObject>();
    [SerializeField] private Sprite m_SlashSprite;
    [SerializeField] private Sprite m_JabSprite;
    [SerializeField] private Sprite m_IceKnifeSprite;
    [SerializeField] private Sprite m_FireballSprite;
    [SerializeField] private Sprite m_EarthTremorSprite;
    [SerializeField] private Sprite m_PoisonSpraySprite;
    [SerializeField] private Sprite m_LightningBoltSprite;
    [SerializeField] private Sprite m_GuidingBoltSprite;
    [SerializeField] private Sprite m_SoundBallSprite;
    [SerializeField] private Sprite m_MissileSprite;
    [SerializeField] private Sprite m_SpikeSprite;
    [SerializeField] private Sprite m_WallSprite;
    [SerializeField] private Sprite m_GustSprite;
    [SerializeField] private Sprite m_HealSprite;
    [SerializeField] private Sprite m_HasteSprite;

    enum WeaponType 
    { 
        MELEE,
        RANGED,
        SPECIAL
    };

    private void Awake()
    {
        BuildDatabase();
    }
    void BuildDatabase()
    {
        InventoryDatabase = new List<WeaponObject>()
        {
            //Each item here will have a NAME, SPRITE, WEAPONTYPE
            new WeaponObject("Sound Ball",m_SoundBallSprite,"Moving ball of sound that bounces off walls",(int)WeaponType.RANGED),
            new WeaponObject("Slash",m_SlashSprite,"Standard slashing motion with a shiny sword (May be Improved in combination with the spin move)",(int)WeaponType.MELEE),
            new WeaponObject("Jab",m_JabSprite,"Standard jabbing motion with a shiny sword",(int)WeaponType.MELEE),
            new WeaponObject("Ice Knife",m_IceKnifeSprite,"Summons knives made of frozen water that are propelled away from you",(int)WeaponType.RANGED),
            new WeaponObject("Fireball",m_FireballSprite,"Summons an elemental ball of fire that burns and damages your enemies",(int)WeaponType.SPECIAL),
            new WeaponObject("Earth Tremor",m_EarthTremorSprite,"Tears the ground up infront of you damaging anyone in the vicinity", (int) WeaponType.SPECIAL),
            new WeaponObject("Poison Spray",m_PoisonSpraySprite,"Spray of the most poisonous potion, it's effects are immediate (May be Improved in combination with the spin move)", (int) WeaponType.SPECIAL),
            new WeaponObject("Lightning Bolt",m_LightningBoltSprite, "Use the power of Thor by summoning lighting with your fingertips",(int) WeaponType.RANGED),
            new WeaponObject("Guiding Bolt",m_GuidingBoltSprite,"Shoots three projectile laser bolts out in a cone shape, good against groups of enemies", (int) WeaponType.RANGED),
            new WeaponObject("Magic Misslile",m_MissileSprite, "A weak dart with that curves towards your enemies",(int) WeaponType.SPECIAL),
            new WeaponObject("Spike Growth",m_SpikeSprite,"Spikes they are spikey and deal damage if enemies are caught in them" ,(int) WeaponType.SPECIAL),
            new WeaponObject("Wall",m_WallSprite,"Summon a wall infront of you to block enemies paths and buy yourself time" ,(int) WeaponType.SPECIAL),
            new WeaponObject("Heal",m_HealSprite,"Use this to heal yourself", (int) WeaponType.SPECIAL),
            new WeaponObject("Haste",m_HasteSprite,"Increases your speed temporarily" ,(int) WeaponType.SPECIAL)

        };
    }
}

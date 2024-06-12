using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{

    public float Maxhealth = 100.0f;
    public static float HealthDecay = 1.0f;
    public static float t_HealthDecay;
    public float DecayRate = 0.25f;
    public bool Death = false;
    public static float CurrentHealth;
    private bool Dodge;
    private bool Decay;
    private PlayerMovement Player;

    static public int Enemys;

    // Start is called before the first frame update
    void Start()
    {
        AddHealth(Maxhealth);
        StartCoroutine(C_LoseHealth());
        Player = GetComponent<PlayerMovement>();
        if (MenuValues.m_Difficulty == 0)
        {
            HealthDecay *= 1.0f;
            t_HealthDecay = HealthDecay;
        }
        else if (MenuValues.m_Difficulty == 1)
        {
            HealthDecay *= 1.5f;
            t_HealthDecay = HealthDecay;
        }
        else
        {
            HealthDecay *= 2f;
            t_HealthDecay = HealthDecay;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Dodge = Player.Dodging;
        //if(Input.GetKeyUp(KeyCode.Space))
        //{
        //    AddHealth(10.0f);
        //    //Debug.Log(CurrentHealth);
        //} //for debugging
    }
    public IEnumerator C_LoseHealth()
    {
        while (!Death)
        {
            yield return new WaitForSeconds(DecayRate);
            Decay = true;
            AddHealth(-t_HealthDecay);
            Decay = false;
        }
        yield return null;
    }
 
    public void AddHealth (float Amount)
    {
        if(Amount > 0 || !Dodge || Decay)
        {
            CurrentHealth += Amount;
            CurrentHealth = Mathf.Clamp(CurrentHealth, 0, Maxhealth);
            if (CurrentHealth <= 0) { Dead(); }

        }
        if(Amount < -t_HealthDecay)
        {
            GetComponentInChildren<Fade>().FadingVignette();
        }

    }
    private void Dead()
    {
        Death = true;
    }
}

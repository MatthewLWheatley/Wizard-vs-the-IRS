using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DylanHealth : MonoBehaviour
{

    public float Maxhealth = 100.0f;
    public float HealthDecay = 1.0f;
    public float DecayRate = 0.25f;
    public bool Death = false;
    private float CurrentHealth;
    // Start is called before the first frame update
    void Start()
    {
        AddHealth(Maxhealth);
        StartCoroutine(C_LoseHealth());
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyUp(KeyCode.Space))
        {
            AddHealth(10.0f);
        }
    }
    private IEnumerator C_LoseHealth()
    {
        while (!Death)
        {
            yield return new WaitForSeconds(DecayRate);
            AddHealth(-HealthDecay);
            Debug.Log(CurrentHealth);
        }
        yield return null;
    }
 
    public void AddHealth (float Amount)
    {
        CurrentHealth += Amount;
        CurrentHealth = Mathf.Clamp(CurrentHealth, 0, Maxhealth);
        if (CurrentHealth <= 0) { Dead(); }
        //Add update healtth bar logic
    }
    private void Dead()
    {
        Death = true;
        Debug.Log("Dead");
    }
}

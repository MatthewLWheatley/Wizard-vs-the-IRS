using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DylanStamina : MonoBehaviour
{
    public float MaxStamina = 100.0f;
    public float CurrentStamina;
    private WaitForSeconds RegenTick = new WaitForSeconds(0.2f);
    private Coroutine RegenStamina;
    private bool Regen = false;
    // Start is called before the first frame update
    void Start()
    {
        CurrentStamina = MaxStamina;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public bool UseStamina(int amount)//The amount of Stamina used is passed into the function
    {
        if (CurrentStamina - amount >= 0)//Checks if the User has Enough stamina to perfom the action
        {
            CurrentStamina -= amount;//Removes the amount of stamina that the action reqiures.
            if (!Regen)//Checks if Stamina is currently recharging
            {
                RegenStamina = StartCoroutine(C_Recharge());
                Regen = true;// Sets Regen flagback to true
            }
            else//Restarts stamina regen so that they dont stack
            {
                Debug.Log("Restart");
                StopCoroutine(RegenStamina);
                RegenStamina = StartCoroutine(C_Recharge());
                Regen = true;// Sets Regen flagback to true
            }
            return true;// Returns true so other functions can know if it has enough stamina to perform action
        }
        else
        {
            return false;// Returns false so other functions know it doesnt have enough stamina to perform action
        }
    }
    private IEnumerator C_Recharge()
    {
        yield return new WaitForSeconds(1.0f);//Wait for a certin amount of time start regening stamina
        while (CurrentStamina < MaxStamina)//Checks if current stamina is less than max so it doesnt go over
        {
            Debug.Log(CurrentStamina);
            CurrentStamina += MaxStamina / 100;// Amount of stamina recovered per Regen Tick
            yield return RegenTick;// Wait the RegenTick amount of time to run code in while loop again.
        }
        Regen = false;// Sets Regen flagback to false
        //CurrentStamna = MaxStamina;
    }
}

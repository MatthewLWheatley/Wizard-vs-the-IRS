using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetCollision : MonoBehaviour
{
    //public GameObject TargetObject;
    public bool TargetTutComplete = false;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "SwordPolyart")
        {
            TargetTutComplete = true;
            //Destroy(TargetObject);
            Debug.Log("Hits");
        }
    }
}

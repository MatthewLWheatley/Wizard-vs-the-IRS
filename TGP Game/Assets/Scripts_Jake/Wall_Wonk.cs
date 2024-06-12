using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall_Wonk : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //randomises the orientation of the "earth" block
        Vector3 newRotation = new Vector3(Random.Range(-2.5f, 2.5f), Random.Range(0f, 360f), Random.Range(-2.5f, 2.5f));
        transform.eulerAngles = newRotation;
    }
}

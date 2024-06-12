using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Corrupt_Earth : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //randomises the orientation of the "earth" block
        Vector3 newRotation = new Vector3(Random.Range(0f, 360f), Random.Range(0f, 360f), Random.Range(0f, 360f));
        transform.eulerAngles = newRotation;
    }
}

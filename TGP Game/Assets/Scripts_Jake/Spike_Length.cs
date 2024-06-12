using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike_Length : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //randomises the orientation of the "earth" block
        Vector3 newPosition = new Vector3(0f, Random.Range(-0.72f, 0.53f), 0f);
        transform.position += newPosition;
    }
}

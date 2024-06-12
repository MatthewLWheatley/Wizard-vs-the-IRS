using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ricochet_Destruction : MonoBehaviour
{
    int bounces = 0;
    [SerializeField] private int maxBounces = 4;
    [SerializeField] private AudioSource source;
    private void OnCollisionEnter(Collision collision)
    {
        bounces++;
        source.Play();
        if (bounces == maxBounces)
        {
            Destroy(gameObject);
        }
    }
}

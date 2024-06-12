using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music_Queue : MonoBehaviour
{
    [SerializeField] private AudioSource source;
    [SerializeField] private AudioClip Track1;
    [SerializeField] private AudioClip Track2;
    int currentTrack = 1;

    void Start()
    {
        source.PlayOneShot(Track1);
    }

    // Update is called once per frame
    void Update()
    {
        if(!source.isPlaying)
        {
            if(currentTrack == 1)
            {
                source.Stop();
                source.PlayOneShot(Track2);
                currentTrack = 2;
            }
            if (currentTrack == 2)
            {
                source.Stop();
                source.PlayOneShot(Track1);
                currentTrack = 1;
            }
        }
    }
}

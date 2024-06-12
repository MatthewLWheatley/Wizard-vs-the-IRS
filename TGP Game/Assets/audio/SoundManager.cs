using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioMixer MyAudioMixer;
    [SerializeField] private string MixerChannel;

    public void SetVolume(float SliderValue)
    {
        MyAudioMixer.SetFloat(MixerChannel, Mathf.Log10(SliderValue) * 20);
    }
}

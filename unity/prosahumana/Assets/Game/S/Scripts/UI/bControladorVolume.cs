using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class bControladorVolume : MonoBehaviour
{
    [SerializeField] private AudioMixerGroup mixer;

    public void SetLevel(float value)
    {
        mixer.audioMixer.SetFloat(name, Mathf.Log10(value) * 20);
    }
}

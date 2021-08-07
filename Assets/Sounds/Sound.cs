using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
[System.Serializable]
public class Sound 
{

    public string name;
    public AudioClip clip;
    [Range(0f,1f)]
    public float volume;
    [Range(0f,1f)]
    public float pitch;
    public AudioSource source;
    public bool loop = false;
}

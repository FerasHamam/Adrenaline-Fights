using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;

    public void Awake()
    {
        foreach(Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }
    public void playSound(string name)
    {
        Sound s = Array.Find(sounds,Sound =>Sound.name == name);
        if(s==null)
        {
            Debug.LogWarning("error! " + name + " not found");
        }
        s.source.Play();
    }
    public void PlaySoundOneShot(string name)
    {
        Sound s = Array.Find(sounds, Sound => Sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("error! " + name + " not found");
        }
        s.source.PlayOneShot(s.source.clip);
    }
    public void pauseSounds()
    {
        foreach(Sound s in sounds)
        {   
            if(s.name != "Touch" || s.name == "windEffect")
                s.source.Pause();
        }
    }
    public void resumeSounds()
    {
        foreach (Sound s in sounds)
        {
            if (s.name == "General" || s.name =="windEffect")
            s.source.Play();
        }
    }
    public void controlSounds(float x)
    {
        foreach(Sound s in sounds)
        {   
            s.source.volume = s.volume * x;
            
        }
    }

}

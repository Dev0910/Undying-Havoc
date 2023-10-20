using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public Sound[] musicScound, sfxScound;
    public AudioSource musicScource, sfxScource;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlayMusic(string name)
    {
        Sound s = Array.Find(musicScound, x => x.name == name);
        
        if(s ==null)
        {
            Debug.Log("Sound not Found");
        }
        else
        {
            musicScource.clip = s.clip;
            musicScource.Play();
        }
    }

    public void PlaySFX(string name)
    {
        Sound s = Array.Find(sfxScound, x => x.name == name);

        if (s == null)
        {
            Debug.Log("Sound not Found");
        }
        else
        {
            sfxScource.PlayOneShot(s.clip);
        }
    }


}

[System.Serializable]
public class Sound
{
    public string name;
    public AudioClip clip;
}

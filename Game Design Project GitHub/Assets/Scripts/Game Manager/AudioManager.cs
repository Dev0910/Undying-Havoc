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

    public void PlaayMusic(string name)
    {
        Sound s = Array.Find(musicScound, x => x.name == name);
            {

        }
    }


}

[System.Serializable]
public class Sound
{
    public string name;
    public Sound clip;
}

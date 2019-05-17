using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumeChange : MonoBehaviour
{
    private AudioSource audiosrc;

    private float musicvolume = 1.0f;

     void Start()
    {
        audiosrc = GetComponent<AudioSource>();
    }

    void Update()
    {
        audiosrc.volume = musicvolume;
    }

    public void SetVolume (float vol)
    {
        musicvolume = vol;
    }

}

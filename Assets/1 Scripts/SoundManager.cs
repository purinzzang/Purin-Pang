using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource[] destroyNoise;
    public AudioSource[] bgms;
    int bgmToPlay;

    public void PlayRandomDestroyNoise()
    {
        if (PlayerPrefs.HasKey("Sound"))
        {
            if (PlayerPrefs.GetInt("Sound") == 1)
            {
                int clipToPlay = Random.Range(0, destroyNoise.Length);
                destroyNoise[clipToPlay].Play();
            }
        }
        else
        {
            int clipToPlay = Random.Range(0, destroyNoise.Length);
            destroyNoise[clipToPlay].Play();
        }
    }

    public void PlayRandomBgm()
    {
        
        bgms[bgmToPlay].Play();
    }

    private void Start()
    {
        bgmToPlay = Random.Range(0, bgms.Length);
    }

    private void Update()
    {
        if (PlayerPrefs.HasKey("Sound"))
        {
            if (PlayerPrefs.GetInt("Sound") == 1)
            {
                bgms[bgmToPlay].volume = 0.3f;
            }
            else
            {
                bgms[bgmToPlay].volume = 0;
            }
        }
        else
        {
            bgms[bgmToPlay].volume = 0.3f;
        }
    }

}

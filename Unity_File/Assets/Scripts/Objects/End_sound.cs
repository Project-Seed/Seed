using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class End_sound : MonoBehaviour
{
    public AudioSource audioSource;

    public AudioClip normal;
    public AudioClip good;
    public AudioClip bad;

    public void normal_play()
    {
        audioSource.clip = normal;
        audioSource.Play();
    }

    public void good_play()
    {
        audioSource.clip = good;
        audioSource.Play();
    }

    public void bad_play()
    {
        audioSource.clip = bad;
        audioSource.Play();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class TimelineController : MonoBehaviour
{
    public PlayableDirector playableDirector;
    public GameObject bridge;
    public void Play()
    {
        playableDirector.Play();
        bridge.SetActive(true);
    }
}

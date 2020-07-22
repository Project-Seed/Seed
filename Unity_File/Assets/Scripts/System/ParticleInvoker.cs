using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleInvoker : MonoBehaviour
{
    public ParticleSystem yellow;
    public ParticleSystem purple;

    public void InvokeYellow()
    {
        yellow.Play();
    }
    public void InvokePurple(Transform transform)
    {
        purple.transform.position = transform.position;
        purple.Play();
    }

    public void InvokeWalkDust(Transform transform)
    {
        purple.transform.position = transform.position;
        purple.Play();
    }
}

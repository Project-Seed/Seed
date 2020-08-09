using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Radio : MonoBehaviour
{
    public bool actives = false;

    public Material blue;

    public MeshRenderer meshRenderer;

    private void Awake()
    {
        fixs();
    }
    public void fixs()
    {
        actives = true;
        GetComponent<TimelineController>().Play();
        meshRenderer.material = blue;
    }
}

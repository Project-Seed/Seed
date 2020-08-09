using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Radio : MonoBehaviour
{
    public bool actives = false;

    public Material blue;

    public MeshRenderer meshRenderer;

    public void fixs()
    {
        actives = true;
        meshRenderer.material = blue;
    }
}

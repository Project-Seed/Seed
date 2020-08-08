using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TerrainManager : MonoBehaviour
{
    public GameObject[] terrain;
    public UnityEvent onCall; //호출당했음

    public void CallTerrain()
    {
        if (onCall != null)
            onCall.Invoke();
    }
}

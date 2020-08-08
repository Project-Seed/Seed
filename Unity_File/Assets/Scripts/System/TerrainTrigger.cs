using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainTrigger : MonoBehaviour
{
    TerrainLoader loader;
    private void Awake()
    {
        loader = GetComponent<TerrainLoader>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        loader.LoadUnLoad();
    }
}

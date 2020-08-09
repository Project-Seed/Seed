using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TerrainManager : MonoBehaviour
{
    public GameObject[] terrains;
    public static TerrainManager instance;
    private TerrainLoader loader;
    public static TerrainManager Instance
    {
        get { return instance; }
    }

    private void Awake()
    {
        instance = this;
    }
    public void CallTerrainManager()
    {
        if (loader = GameObject.Find(GameSystem.instance.map_name).GetComponent<TerrainLoader>())
            loader.LoadUnLoad();
    }
}

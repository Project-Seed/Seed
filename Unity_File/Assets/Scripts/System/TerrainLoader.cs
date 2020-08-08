﻿using UnityEngine;

public class TerrainLoader : MonoBehaviour
{
    public GameObject[] loadTerrains;
    public GameObject[] unloadTerrains;

    public void LoadUnLoad()
    {
        if (!loadTerrains.Length.Equals(0))
            for (int i = 0; i < loadTerrains.Length; ++i)
            {
                Debug.Log("Load" + loadTerrains[i].name);
                loadTerrains[i].SetActive(true);
            }

        if (!unloadTerrains.Length.Equals(0))
            for (int j = 0; j < unloadTerrains.Length; ++j)
            {
                Debug.Log("UnLoad" + unloadTerrains[j].name);
                unloadTerrains[j].SetActive(false);
            }
    }

    public void Load()
    {
        if (loadTerrains.Length.Equals(0)) return;
        for (int i = 0; i < loadTerrains.Length; ++i)
            loadTerrains[i].SetActive(true);
    }

    public void UnLoad()
    {
        if (unloadTerrains.Length.Equals(0)) return;
        for (int i = 0; i < unloadTerrains.Length; ++i)
            unloadTerrains[i].SetActive(false);
    }
}


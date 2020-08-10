using UnityEngine;

public class TerrainLoader : MonoBehaviour
{
    public GameObject[] loadTerrains;
    public GameObject[] unloadTerrains;

    public void LoadUnLoad()
    {
        if (!unloadTerrains.Length.Equals(0))
            for (int j = 0; j < unloadTerrains.Length; ++j)
            {
                unloadTerrains[j].SetActive(false);
            }
        if (!loadTerrains.Length.Equals(0))
            for (int i = 0; i < loadTerrains.Length; ++i)
            {
                loadTerrains[i].SetActive(true);
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


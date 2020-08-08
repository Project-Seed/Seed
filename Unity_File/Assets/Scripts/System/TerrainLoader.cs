using UnityEngine;

class TerrainLoader : MonoBehaviour
{
    public GameObject[] terrain;
   // private MeshRenderer renderer;

    //private void Start()
    //{
    //    renderer = GetComponent<MeshRenderer>();
    //    renderer.enabled = false;
    //}

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.tag.Equals("Player"))
    //    {
    //        for (int i = 0; i < terrain.Length; ++i)
    //            terrain[i].SetActive(true);
    //    }
    //}

    public void UnLoad()
    {
        for (int i = 0; i < terrain.Length; ++i)
            terrain[i].SetActive(true);
    }
}


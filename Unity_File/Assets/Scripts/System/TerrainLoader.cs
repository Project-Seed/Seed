using UnityEngine;

class TerrainLoader : MonoBehaviour
{

    public GameObject terrain;
    private string tag;
    private MeshRenderer renderer;

    private void Start()
    {
        tag = terrain.tag;
        renderer = GetComponent<MeshRenderer>();
        renderer.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            terrain.SetActive(true);
        }
    }
}


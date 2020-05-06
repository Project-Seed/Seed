using UnityEngine;

class TerrainUnloader : MonoBehaviour
{

    public GameObject[] terrain;
    private MeshRenderer renderer;

    private void Start()
    {
        renderer = GetComponent<MeshRenderer>();
        renderer.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            for (int i = 0; i < terrain.Length; ++i)
                terrain[i].SetActive(false);
        }
    }
}


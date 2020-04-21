using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class Plant : MonoBehaviour
{
    Vector3 position { get; set; }
    Quaternion rotation { get; set; }

    string type;
    public GameObject plant;

    public string seed_name;

    private void Start()
    {
        plant = Resources.Load(seed_name, typeof(GameObject)) as GameObject;

    }

    public void PlantSeed(Vector3 pos, Vector3 normal)
    {
        if (!plant)
        {
            Debug.LogError("Missing Plant");
            return;
        }
        GameObject obj;

        //식물 모델 위치에 생성
        obj = Instantiate(plant);
        obj.transform.position = pos;
        obj.transform.forward = normal;

        Debug.Log("Planted");
    }
}

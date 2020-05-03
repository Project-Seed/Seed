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
    public Vector3 red_go;

    private void Start()
    {
        plant = Resources.Load(seed_name, typeof(GameObject)) as GameObject;

    }

    public void PlantSeed(Vector3 pos, Vector3 normal, bool red)
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

        if(red == true)
        {
            obj.transform.rotation = Quaternion.LookRotation(red_go);
            obj.transform.rotation = Quaternion.Euler(new Vector3(0, obj.transform.rotation.eulerAngles.y, 0));
        }

        Debug.Log("Planted");
    }
}

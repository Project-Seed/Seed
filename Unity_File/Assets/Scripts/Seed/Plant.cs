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
    public Vector3 red_go; // 빨간 씨앗이 자라는 방향

    public MeshFilter meshFilter;
    public MeshRenderer meshRenderer;

    public List<Mesh> meshes;
    public List<Material> materials;

    private void Start()
    {
        plant = Resources.Load(seed_name, typeof(GameObject)) as GameObject;

        switch(seed_name)
        {
            case "blue_seed":
                meshFilter.mesh = meshes[0];
                meshRenderer.material = materials[0];
                break;

            case "brown_seed":
                meshFilter.mesh = meshes[1];
                meshRenderer.material = materials[1];
                break;

            case "green_seed":
                meshFilter.mesh = meshes[2];
                meshRenderer.material = materials[2];
                break;

            case "lime_seed":
                meshFilter.mesh = meshes[3];
                meshRenderer.material = materials[3];
                break;

            case "purple_seed":
                meshFilter.mesh = meshes[4];
                meshRenderer.material = materials[4];
                break;

            case "red_seed":
                meshFilter.mesh = meshes[5];
                meshRenderer.material = materials[5];
                break;

            case "white_seed":
                meshFilter.mesh = meshes[6];
                meshRenderer.material = materials[6];
                break;

            case "yellow_seed":
                meshFilter.mesh = meshes[7];
                meshRenderer.material = materials[7];
                break;
        }
    }

    public void PlantSeed(Vector3 pos, Vector3 normal, bool red, bool blue)
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
            obj.transform.LookAt(GameObject.Find("Player").transform.position);
            obj.transform.rotation = Quaternion.Euler(new Vector3(0, obj.transform.rotation.eulerAngles.y + 180, 0));
        }
        else if(blue == true)
        {
            obj.transform.rotation = Quaternion.Euler(new Vector3(0, obj.transform.rotation.eulerAngles.y, obj.transform.rotation.eulerAngles.z));
        }
    }

    public void PlantFail(Vector3 pos, Vector3 normal,string name)
    {
        GameObject obj;
        //식물 모델 위치에 생성
        obj = Instantiate(plant);//이거를 파티클로바꾸기.
        obj.transform.position = pos;
        obj.transform.forward = normal;
    }
}

using System;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject boom;
    public GameObject scene;
    ParticleInvoker particleInvoker;
    Plant plant;
    private void Start()
    {
        plant = GetComponent<Plant>();
        particleInvoker = FindObjectOfType<ParticleInvoker>();
    }

    private void SetPlantPos(Collision collision)
    {
        ContactPoint point = collision.GetContact(0);
        Vector3 pos = point.point;
        Vector3 normal = point.normal;

        //pos는 Ray 쏜 곳.
        if (plant.seed_name == "red_seed" || plant.seed_name == "white_seed")
        {
            plant.PlantSeed(pos, normal, true, false);
            plant.red_go = gameObject.transform.position;
        }
        else if (plant.seed_name == "blue_seed")
            plant.PlantSeed(pos, normal, false, true);
        else
            plant.PlantSeed(pos, normal, false, false);
    }

    private static bool checkWall(ref Collision hit,string type)
    {
        float angle = Vector3.Angle(hit.GetContact(0).normal, Vector3.up);
        Debug.Log("hit " + hit.transform.gameObject.name + "Angle " + angle);
        bool isWall;
        if (type == "blue_seed")
        {
            if (70.0f <= angle && angle < 130.0f)
                isWall = true;
            else
                isWall = false;
        }
        else if (type == "brown_seed")
        {
            if (60.0f <= angle && angle < 90.0f)
                isWall = true;
            else
                isWall = false;
        }
        else if (0.0f <= angle && angle < 50.0f)
            isWall = false;
        else //blue, brown 아닌데 50도 이상이면 벽으로 인식
            isWall = true;
        return isWall;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
        bool isWall = checkWall(ref collision, plant.seed_name);

        switch (plant.seed_name)
        {
            case "blue_seed":
            case "brown_seed":
                if (isWall)
                    SetPlantPos(collision);
                break;

            case "red_seed":
            case "white_seed":
                if (!isWall)
                    SetPlantPos(collision);
                break;

            case "green_seed":
                if (collision.gameObject.CompareTag("Plant2"))
                {
                    collision.transform.localScale =
                            new Vector3(collision.transform.localScale.x * 2,
                            collision.transform.localScale.y * 2,
                            collision.transform.localScale.z * 2);

                    Transform t;
                    if (collision.gameObject.name == "brown_seed")
                    {
                        t = collision.gameObject.transform.parent.GetChild(1);

                        t.localScale = new Vector3(t.localScale.x * 2,
                                t.localScale.y * 2,
                                t.localScale.z * 2);
                    }
                }
                break;

            case "lime_seed":
                if (collision.gameObject.CompareTag("Plant2") || collision.gameObject.name == "brown_seed")
                    collision.transform.localScale =
                            new Vector3(collision.transform.localScale.x / 2,
                            collision.transform.localScale.y / 2,
                            collision.transform.localScale.z / 2);
                break;

            case "yellow_seed":
                if (collision.gameObject.CompareTag("Yellow"))
                    collision.gameObject.GetComponent<Rigidbody>().mass = 1;
                break;

            case "purple_seed":
                if (collision.gameObject.CompareTag("Purple"))
                {
                    collision.gameObject.SetActive(false);
                    particleInvoker.InvokePurple(collision.gameObject.transform);
                }
                //Instantiate(boom, gameObject.transform.position, gameObject.transform.rotation);
                //particleInvoker.InvokePurple();
                break;

            default: break;

        }
    }
}
       
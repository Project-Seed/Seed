using System;
using System.Collections.Generic;
using UnityEngine;
public class Bullet : MonoBehaviour
{
    public GameObject boom;
    public GameObject scene;
    Plant plant;

    private void Start()
    {
        plant = GetComponent<Plant>();
    }

    private void SetPlantPos(Collision collision)
    {
        ContactPoint point = collision.GetContact(0);
        Vector3 pos = point.point;
        Vector3 normal = point.normal;

        //pos는 Ray 쏜 곳.
        if(plant.seed_name == "red_seed" || plant.seed_name == "white_seed")
            plant.PlantSeed(pos, normal, true);
        else
            plant.PlantSeed(pos, normal, false);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);

        switch (plant.seed_name)
        {
            case "blue_seed":
            case "brown_seed":
                if (collision.gameObject.CompareTag("Plantable"))
                    SetPlantPos(collision);
                break;

            case "red_seed":
            case "white_seed":
                if (collision.gameObject.CompareTag("Plantable") || collision.gameObject.CompareTag("Ground"))
                    SetPlantPos(collision);
                break;

            case "green_seed":
                if (collision.gameObject.CompareTag("Plant"))
                    collision.transform.localScale =
                            new Vector3(collision.transform.localScale.x * 2,
                            collision.transform.localScale.y * 2,
                            collision.transform.localScale.z * 2);
                break;

            case "lime_seed":
                if (collision.gameObject.CompareTag("Plant"))
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
                Instantiate(boom, gameObject.transform.position, gameObject.transform.rotation);
                break;

            default: break;

        }
    }
}
       
using System;
using System.Collections.Generic;
using UnityEngine;
public class Bullet : MonoBehaviour
{
    Plant plant;
    private void Start()
    {
        plant = GetComponent<Plant>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (plant.seed_name == "blue_seed" || plant.seed_name == "brown_seed" || plant.seed_name == "red_seed")
        {
            if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Plantable"))
            {
                Debug.Log("Plant");
                ContactPoint point = collision.GetContact(0);
                Vector3 pos = point.point;
                Vector3 normal = point.normal;
                Debug.Log("Nomal : " + normal);
                plant.PlantSeed(pos, normal);

                gameObject.SetActive(false);
            }
        }
        else if(plant.seed_name == "green_seed")
        {
            if (collision.gameObject.CompareTag("Plant"))
            {
                collision.transform.localScale = new Vector3(collision.transform.localScale.x * 2, collision.transform.localScale.y * 2, collision.transform.localScale.z * 2);

                gameObject.SetActive(false);
            }
        }
        else if (plant.seed_name == "yellow_seed")
        {
            if (collision.gameObject.CompareTag("Plant"))
            {
                Debug.Log("Plant");
                ContactPoint point = collision.GetContact(0);
                Vector3 pos = point.point;
                Vector3 normal = point.normal;
                Debug.Log("Nomal : " + normal);
                plant.PlantSeed(pos, normal);

                gameObject.SetActive(false);
            }
        }
    }
}


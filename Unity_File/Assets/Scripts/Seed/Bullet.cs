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
        if (plant.seed_name == "blue_seed" || plant.seed_name == "brown_seed")
        {
            if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Plantable"))
            {
                ContactPoint point = collision.GetContact(0);
                Vector3 pos = point.point;
                Vector3 normal = point.normal;

                //pos는 Ray 쏜 곳.
                plant.PlantSeed(pos, normal, false);

                gameObject.SetActive(false);
            }
        }
        else if (plant.seed_name == "red_seed")
        {
            if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Plantable"))
            {
                ContactPoint point = collision.GetContact(0);
                Vector3 pos = point.point;
                Vector3 normal = point.normal;
                plant.PlantSeed(pos, normal, true);

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
        else if (plant.seed_name == "lime_seed")
        {
            if (collision.gameObject.CompareTag("Plant"))
            {
                collision.transform.localScale = new Vector3(collision.transform.localScale.x / 2, collision.transform.localScale.y / 2, collision.transform.localScale.z / 2);

                gameObject.SetActive(false);
            }
        }
        else if (plant.seed_name == "yellow_seed")
        {
            if (collision.gameObject.CompareTag("Plant"))
            {
                /* 전 노랑씨앗
                Debug.Log("Plant");
                ContactPoint point = collision.GetContact(0);
                Vector3 pos = point.point;
                Vector3 normal = point.normal;
                Debug.Log("Nomal : " + normal);
                plant.PlantSeed(pos, normal, false);
                */

                gameObject.SetActive(false);
            }
        }
        else if (plant.seed_name == "purple_seed")
        {
            if (collision.gameObject.CompareTag("Ground"))
            {
                gameObject.SetActive(false);
            }
        }
    }
}


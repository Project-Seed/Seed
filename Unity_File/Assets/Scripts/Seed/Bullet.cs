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
        if(collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Plantable"))
        {
            ContactPoint point = collision.GetContact(0);
            Vector3 pos = point.point;
            Vector3 normal = point.normal;
            plant.PlantSeed(pos, normal);

            transform.gameObject.SetActive(false);
        }
    }
}


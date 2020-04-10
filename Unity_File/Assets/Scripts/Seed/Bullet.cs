using System;
using System.Collections.Generic;
using UnityEngine;
public class Bullet : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Plantable"))
        {
            Debug.Log("Plant");
            ContactPoint point = collision.GetContact(0);
            Vector3 pos = point.point;
            Vector3 nomal = point.normal;

            transform.gameObject.SetActive(false);
        }
    }
}


using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gress2 : MonoBehaviour
{
    bool trigger = false;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "Player")
            trigger = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            trigger = false;
            gameObject.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (trigger && other.gameObject.name == "Player")
        {
            gameObject.transform.LookAt(other.gameObject.transform);
            gameObject.transform.rotation = Quaternion.Euler(new Vector3(45 - (DistanceToPoint(gameObject.transform.position, other.transform.position) * 100), gameObject.transform.eulerAngles.y, 0));
        }
    }

    public float DistanceToPoint(Vector3 a, Vector3 b)
    {
        return (float)Math.Sqrt(Math.Pow(a.x - b.x, 2) + Math.Pow(a.z - b.z, 2));
    }
}

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapChecker : MonoBehaviour
{
    void Start()
    {
    }

    void Update()
    {
        MapCheck(5.0f);
    }

    public bool MapCheck(float distance)
    {
        if (Physics.Raycast(transform.position, -transform.up, out RaycastHit hit, distance))
        {
            Debug.DrawRay(transform.position, -transform.up, Color.green, 2f);
            //Debug.Log("Map Name : " + hit.collider.gameObject.name);
            GameSystem.instance.map_name = hit.collider.gameObject.name;
            return true;
        }
        return false;

    }
}
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapChecker : MonoBehaviour
{
    void Update()
    {
        MapCheck(5.0f);
    }

    public bool MapCheck(float distance)
    {
        if (Physics.Raycast(transform.position, -transform.up, out RaycastHit hit, distance))
        {
            if(hit.collider.gameObject.CompareTag("Ground"))
            {
                GameSystem.instance.map_name = hit.collider.gameObject.name;
            }
            return true;
        }
        return false;
    }
}

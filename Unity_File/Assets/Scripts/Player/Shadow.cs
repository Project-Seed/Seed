﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shadow : MonoBehaviour
{
    public PlayerController playerController;


    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.name == "brown_trigger")
        {
            playerController.shadow_out();
        }
    }
}

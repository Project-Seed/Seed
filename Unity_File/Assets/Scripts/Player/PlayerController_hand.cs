using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController_hand : MonoBehaviour
{
    public PlayerController pc;


    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.name == "blue_trigger")
        {
            pc.hang_crash = true;
            Debug.Log("ddd");
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.name == "blue_trigger")
        {
            pc.hang_crash = false;
        }
    }
}

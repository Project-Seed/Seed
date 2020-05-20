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

            Key_guide.instance.climb_on();
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.name == "blue_trigger")
        {
            pc.hang_crash = false;

            Key_guide.instance.climb_off();
        }
    }
}

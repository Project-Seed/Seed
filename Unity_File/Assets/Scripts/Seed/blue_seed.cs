using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class blue_seed : MonoBehaviour
{
    public PlayerController pc;
    public GameObject blue;

    private void Awake()
    {
        pc = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.name == "Player")
        {
            pc.hang_ob = blue;
            pc.hang_crash = true;

            Key_guide.instance.climb_on();
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.name == "Player")
        {
            pc.hang_crash = false;

            Key_guide.instance.climb_off();
        }
    }
}

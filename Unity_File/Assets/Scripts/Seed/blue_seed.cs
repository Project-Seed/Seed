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

            if (gameObject.name == "left")
                pc.hang_vecter = 0;
            else
                pc.hang_vecter = 1;

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

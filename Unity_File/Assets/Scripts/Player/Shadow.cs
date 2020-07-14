using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shadow : MonoBehaviour
{
    public PlayerController playerController;

    bool a = true;

    private void OnEnable()
    {
        a = true;
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.name == "brown_trigger" && a)
        {
            a = false;
            StartCoroutine(playerController.climb_up());
        }
    }
}

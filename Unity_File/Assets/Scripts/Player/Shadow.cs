using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shadow : MonoBehaviour
{
    public PlayerController playerController;


    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.name == "brown_trigger")
        {
            Debug.Log("갈색 떨어짐");
            playerController.climb_crash = false;

            playerController.shadow2.SetActive(false);
            playerController.shadow.SetActive(false);
        }
    }
}

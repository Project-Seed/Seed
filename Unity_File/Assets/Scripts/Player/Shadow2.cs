using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shadow2 : MonoBehaviour
{
    public PlayerController playerController;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Plantable"))
        {
            playerController.shadow_out();
        }
    }
}

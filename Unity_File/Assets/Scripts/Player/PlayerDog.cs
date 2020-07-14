using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDog : MonoBehaviour
{
    public PlayerController playerController;
    public PlayerState player_state;

    private void Awake()
    {
    }

    private void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ground") || other.gameObject.CompareTag("Plantable") ||
            other.gameObject.CompareTag("Purple") || other.gameObject.CompareTag("Yellow"))
        {
            playerController.is_jumping = false;
            Debug.Log("in Ground");

            player_state.landing();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Ground") || other.gameObject.CompareTag("Plantable") ||
            other.gameObject.CompareTag("Purple") || other.gameObject.CompareTag("Yellow"))
        {
            Debug.Log("not in Ground");

            player_state.flying();
        }
    }
}

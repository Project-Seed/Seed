using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ButtonCollider : MonoBehaviour
{
    public UnityEvent onEnter;
    public UnityEvent onExit;
    public UnityEvent onKeyE;
    public bool playOnes;

    public void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        if (onEnter != null)
            onEnter.Invoke();

        if (playOnes)
            gameObject.SetActive(false);
    }
    public void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        if (onExit != null)
            onExit.Invoke();

        if (playOnes)
            gameObject.SetActive(false);
    }

    public void OnTriggerStay(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (onKeyE != null)
            {
                onKeyE.Invoke();
            }
        }

        if (playOnes)
            gameObject.SetActive(false);
    }
}

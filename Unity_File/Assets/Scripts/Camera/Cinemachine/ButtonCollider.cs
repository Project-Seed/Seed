using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ButtonCollider : MonoBehaviour
{
    public UnityEvent onEnter;
    public UnityEvent onSpacebar;
    public UnityEvent onReturn;
    public bool playOnes;

    public void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        if (onEnter != null)
            onEnter.Invoke();

        if (playOnes)
            gameObject.SetActive(false);
    }

    //public void OnTriggerStay()
    //{
    //    if (Input.GetKeyDown(KeyCode.Space))
    //    {
    //        if (onSpacebar != null)
    //        {
    //            onSpacebar.Invoke();
    //        }
    //    }

    //    if (Input.GetKeyDown(KeyCode.Return))
    //    {
    //        if (onReturn != null)
    //        {
    //            onReturn.Invoke();
    //        }
    //    }
    }
}

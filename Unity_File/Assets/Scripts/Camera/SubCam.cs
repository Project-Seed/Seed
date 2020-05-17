using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubCam : MonoBehaviour
{
    public Transform target;

    private void LateUpdate()
    {
        transform.localRotation.SetLookRotation(target.forward);
    }

}

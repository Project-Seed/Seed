using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubCam : MonoBehaviour
{

    public void SwitchCam(Camera camA, Camera camB)
    {
        //smooth주기
        camA.enabled = false;
        camB.enabled = true;
        
    }
}

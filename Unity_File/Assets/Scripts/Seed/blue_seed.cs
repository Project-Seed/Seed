using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class blue_seed : MonoBehaviour
{
    float num = 0.5f;

    private void Awake()
    {
        gameObject.GetComponent<Renderer>().sharedMaterial.SetFloat("_Grow", num);
    }

    void Update()
    {
        if (num > -0.5f)
        {
            num -= 0.25f*Time.deltaTime;
            gameObject.GetComponent<Renderer>().sharedMaterial.SetFloat("_Grow", num);
        }
    }
}

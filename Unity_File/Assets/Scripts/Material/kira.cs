using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class kira : MonoBehaviour
{
    public float r;
    public float g;
    public float b;

    int i = 0;
    bool ch = false;

    IEnumerator Start()
    {
        while (true)
        {
            gameObject.GetComponent<MeshRenderer>().material.color = new Color(r * (i / 50), g * (i / 50), b * (i / 50));

            if (ch == false)
                i++;
            else
                i--;

            if (i == 50)
                ch = true;
            else if (i == 0)
                ch = false;

            yield return new WaitForSeconds(0.01f);
        }
    }

    void Update()
    {

    }
}

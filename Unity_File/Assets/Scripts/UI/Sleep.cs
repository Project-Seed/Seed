using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sleep : MonoBehaviour
{
    public Image image;

    void Start()
    {
        StartCoroutine(view());
    }

    IEnumerator view()
    {
        for (int i = 0; i <= 50; i++)
        {
            image.color = new Color(0, 0, 0, i / 50f);

            yield return new WaitForSeconds(0.01f);
        }
    }
}

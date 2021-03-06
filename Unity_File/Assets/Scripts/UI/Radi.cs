﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Radi : MonoBehaviour
{
    public Image left;
    public Image right;

    public GameObject left_ob;
    public GameObject right_ob;


    public void radi_up(int num) // 1이면 우측, 0이면 좌측
    {
        if (num == 1)
            StartCoroutine(radi_up_co(right, right_ob));
        else
            StartCoroutine(radi_up_co(left, left_ob));
    }


    IEnumerator radi_up_co(Image image, GameObject ob)
    {
        for (int i = 0; i <= 20; i++)
        {
            if (i <= 5)
                image.color = new Color(i / 5f, i / 5f, i / 5f);
            else if (i >= 10)
                image.color = new Color(1.5f - (i / 20f), (10 - (i - 10)) / 10f, 1);
            yield return new WaitForSeconds(0.01f);
        }
    }


    public void radi_down(int num) // 1이면 우측, 0이면 좌측
    {
        if (num == 1)
            StartCoroutine(radi_down_co(right, right_ob));
        else
            StartCoroutine(radi_down_co(left, left_ob));
    }


    IEnumerator radi_down_co(Image image, GameObject ob)
    {
        for (int i = 0; i <= 20; i++)
        {
            if (i <= 5)
                image.color = new Color(0.5f + (i / 10f), i / 5f, 1);
            else if (i >= 10)
                image.color = new Color((10 - (i - 10)) / 10f, (10 - (i - 10)) / 10f, (10 - (i - 10)) / 10f);
            yield return new WaitForSeconds(0.01f);
        }
    }
}

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Heart : MonoBehaviour
{
    public Image left;
    public Image right;

    public GameObject left_ob;
    public GameObject right_ob;


    public void hp_up(int num) // 1이면 우측, 0이면 좌측
    {
        if (num == 1)
            StartCoroutine(hp_up_co(right, right_ob));
        else
            StartCoroutine(hp_up_co(left, left_ob));
    }

    IEnumerator hp_up_co(Image image, GameObject ob)
    {
        for (int i = 0; i <= 20; i++)
        {
            if (i <= 5)
                image.color = new Color(i / 5f, i / 5f, i / 5f);
            else if (i >= 10)
                image.color = new Color(i, (10 - (i - 10)) / 10f, (10 - (i - 10)) / 10f);
            yield return new WaitForSeconds(0.01f);
        }
    }


    public void hp_down(int num) // 1이면 우측, 0이면 좌측
    {
        if (num == 1)
            StartCoroutine(hp_down_co(right, right_ob));
        else
            StartCoroutine(hp_down_co(left, left_ob));
    }

    IEnumerator hp_down_co(Image image, GameObject ob)
    {
        GameObject.Find("Player").GetComponent<PlayerController>().player_mate.color = new Color(1f, 0f, 0f, 1);

        for (int i = 0; i <= 20; i++)
        {
            if (i <= 5)
            {
                image.color = new Color(1, i / 5f, i / 5f);
                GameObject.Find("Player").GetComponent<PlayerController>().player_mate.color = new Color(1f, (5-i) / 5f, (5 - i) / 5f, 1);
            }
            else if (i >= 10)
            {
                image.color = new Color((10 - (i - 10)) / 10f, (10 - (i - 10)) / 10f, (10 - (i - 10)) / 10f);
            }

            if(i > 5)
                GameObject.Find("Player").GetComponent<PlayerController>().player_mate.color = new Color(1f, (i - 5) / 15f, (i - 5) / 15f, 1);

            yield return new WaitForSeconds(0.01f);
        }

        GameObject.Find("Player").GetComponent<PlayerController>().player_mate.color = new Color(1f, 1f, 1f, 1);
    }
}
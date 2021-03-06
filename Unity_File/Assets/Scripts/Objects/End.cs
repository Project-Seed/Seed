﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.UI;

public class End : MonoBehaviour
{
    public List<Image> image;

    public bool click_ok = false;
    public bool your_good = false;

    private void Start()
    {
        StartCoroutine(wake_up());
    }

    IEnumerator wake_up()
    {
        InputManager.instance.game_stop();

        if(your_good)
            GameObject.Find("BGM").GetComponent<End_sound>().good_play();
        else
            GameObject.Find("BGM").GetComponent<End_sound>().bad_play();


        yield return new WaitForSeconds(2f);


        for (int j = 0; j < 4; j++)
        {
            for (int i = 0; i <= 100; i++)
            {
                image[j].color = new Color(1, 1, 1, i / 100f);
                yield return new WaitForSeconds(0.01f);
            }

            yield return new WaitForSeconds(1.5f);
        }

        click_ok = true;
    }

    private void OnEnable()
    {
        for (int j = 0; j < 4; j++)
        {
            image[j].color = new Color(1, 1, 1, 0f);
        }
    }

    public void click()
    {
        if(click_ok)
        {
            click_ok = false;

            StartCoroutine(wake_up2());
        }
    }

    IEnumerator wake_up2()
    {
        for (int j = 0; j < 3; j++)
        {
            image[j].color = new Color(1, 1, 1, 0f);
        }

        for (int i = 0; i <= 100; i++)
        {
            image[3].color = new Color(1, 1, 1, (100-i) / 100f);
            yield return new WaitForSeconds(0.01f);
        }

        InputManager.instance.game_start();
        GameObject.Find("BGM").GetComponent<End_sound>().normal_play();
        gameObject.SetActive(false);   
    }
}

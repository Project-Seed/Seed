using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.UI;

public class End : MonoBehaviour
{
    public Image image;

    public List<Sprite> sprites;

    public bool click_ok = false;

    private void Start()
    {
        StartCoroutine(wake_up());
    }

    IEnumerator wake_up()
    {
        InputManager.instance.game_stop();

        for (int i=0; i<=100; i++)
        {
            image.color = new Color(1, 1, 1, i / 100f);
            yield return new WaitForSeconds(0.01f);
        }

        for (int i = 0; i < 4; i++)
        {
            image.sprite = sprites[i];
            yield return new WaitForSeconds(2f);
        }

        click_ok = true;
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
        for (int i = 0; i <= 100; i++)
        {
            image.color = new Color(1, 1, 1, (100-i) / 100f);
            yield return new WaitForSeconds(0.01f);
        }

        InputManager.instance.game_start();
        gameObject.SetActive(false);   
    }
}

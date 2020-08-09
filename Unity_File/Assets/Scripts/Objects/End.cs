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
            gameObject.SetActive(false);
        }
    }
}

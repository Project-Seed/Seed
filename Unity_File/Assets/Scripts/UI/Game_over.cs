using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game_over : MonoBehaviour
{
    public Image image;

    private void OnEnable()
    {
        image.color = new Color(1, 1, 1, 0);
    }

    public void image_on()
    {
        StartCoroutine(on());
    }

    IEnumerator on()
    {
        for (int i = 0; i <= 150; i++)
        {
            image.color = new Color(1, 1, 1, i / 150f);
            yield return new WaitForSeconds(0.01f);
        }
    }
}

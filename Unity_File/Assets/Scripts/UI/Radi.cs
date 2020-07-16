using System.Collections;
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
            StartCoroutine(change(right, right_ob));
        else
            StartCoroutine(change(left, left_ob));
    }


    IEnumerator change(Image image, GameObject ob)
    {
        for (int i = 0; i < 20; i++)
        {
            if (i <= 5)
                image.color = new Color(i / 5f, i / 5f, i / 5f);
            else if (i >= 10)
                image.color = new Color(1.5f - (i / 20f), (10 - (i - 10)) / 10f, 1);
            yield return new WaitForSeconds(0.01f);
        }
    }
}

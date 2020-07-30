using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Bleeding : MonoBehaviour
{
    private Image img;
    void Start()
    {
        img = GetComponent<Image>();
        img.enabled = false;
    }

    IEnumerator Bleed(float bleedingTime)
    {
        float sumTime = 0.0f;
        float alpha = 0.0f;

        while (sumTime < bleedingTime / 2)
        {
            alpha += Time.deltaTime / bleedingTime;
            img.color = new Color(1f, 1f, 1f, alpha);
            sumTime += Time.deltaTime / bleedingTime;
            if (alpha >= 1)
            {
                alpha = 1;
            }
            yield return null;
        }
        while (sumTime >= bleedingTime / 2 && sumTime < bleedingTime)
        {
            alpha -= Time.deltaTime / bleedingTime; 
            img.color = new Color(1f, 1f, 1f, alpha);
            sumTime += Time.deltaTime / bleedingTime;
            if (alpha <= 0)
            {
                alpha = 0;
            }
            yield return null;
        }
       
    }

    private void HPFine()
    {
        //다시 괜찮아졌을때
        img.enabled = false;
    }

    public void Attacked()
    {
        //잠깐 출혈있을 때 생겼다 사라지기.
        img.enabled = true;
        StartCoroutine(Bleed(1.0f));
    }
}

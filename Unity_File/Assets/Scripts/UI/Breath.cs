using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Breath : MonoBehaviour
{
    public List<Image> images;
    public List<Text> texts;

    int i = 0;
    IEnumerator Start()
    {
        while(true)
        {
            if (i < 25)
            {
                for (int j = 0; j < images.Count; j++)
                    images[j].color = new Color(images[j].color.r, images[j].color.g, images[j].color.b, i / 25f);
                for (int k = 0; k < texts.Count; k++)
                    texts[k].color = new Color(texts[k].color.r, texts[k].color.g, texts[k].color.b, i / 25f);
            }
            else
            {
                for (int j = 0; j < images.Count; j++)
                    images[j].color = new Color(images[j].color.r, images[j].color.g, images[j].color.b, (100 - i) / 75f);
                for (int k = 0; k < texts.Count; k++)
                    texts[k].color = new Color(texts[k].color.r, texts[k].color.g, texts[k].color.b, (100 - i) / 75f);
            }

            if (i < 100)
                i++;
            else
                i = 0;
            yield return new WaitForSeconds(0.01f);
        }
    }
}

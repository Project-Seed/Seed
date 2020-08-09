using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Loading_tip : MonoBehaviour
{
    public List<GameObject> images;

    private void OnEnable()
    {
        int num = Random.Range(0, images.Count);

        if(num == 0)
        {
            images[0].SetActive(true);
            images[1].SetActive(false);
        }

        if (num == 1)
        {
            images[1].SetActive(true);
            images[0].SetActive(false);
        }
    }
}

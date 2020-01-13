using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Note : MonoBehaviour
{
    public GameObject illustrated;
    public GameObject diary;


    private void Awake()
    {
        gameObject.SetActive(false);
    }

    public void On_illustrated()
    {
        illustrated.SetActive(true);
        diary.SetActive(false);
    }

    public void On_diary()
    {
        illustrated.SetActive(false);
        diary.SetActive(true);
    }
}

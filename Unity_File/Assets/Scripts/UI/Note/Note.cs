using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Note : MonoBehaviour
{
    public GameObject illustrated;
    public GameObject diary;
    public GameObject map;


    private void Awake()
    {
        gameObject.SetActive(false);
    }

    public void On_illustrated()
    {
        illustrated.SetActive(true);
        diary.SetActive(false);
        map.SetActive(false);
    }

    public void On_diary()
    {
        illustrated.SetActive(false);
        diary.SetActive(true);
        map.SetActive(false);
    }

    public void On_map()
    {
        illustrated.SetActive(false);
        diary.SetActive(false);
        map.SetActive(true);
    }
}

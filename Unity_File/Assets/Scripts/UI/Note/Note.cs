using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Note : MonoBehaviour
{
    public GameObject illustrated;
    public GameObject diary;
    public GameObject map;
    public GameObject quest;


    private void Awake()
    {
        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        InputManager.instance.click_mod = 1;
    }
    private void OnDisable()
    {
        InputManager.instance.click_mod = 0;
    }

    public void On_illustrated()
    {
        illustrated.SetActive(true);
        diary.SetActive(false);
        map.SetActive(false);
        quest.SetActive(false);
    }

    public void On_diary()
    {
        illustrated.SetActive(false);
        diary.SetActive(true);
        map.SetActive(false);
        quest.SetActive(false);
    }

    public void On_map()
    {
        illustrated.SetActive(false);
        diary.SetActive(false);
        map.SetActive(true);
        quest.SetActive(false);
    }

    public void On_quest()
    {
        illustrated.SetActive(false);
        diary.SetActive(false);
        map.SetActive(false);
        quest.SetActive(true);
    }

    public void close_note()
    {
        gameObject.SetActive(false);
    }
}

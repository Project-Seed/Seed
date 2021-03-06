﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Eat_system : MonoBehaviour
{
    public static Eat_system instance;
    public GameObject view;
    public GameObject eat_box;
    public List<GameObject> items;

    public static Eat_system Instance
    {
        get { return instance; }
    }

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        if(items.Count > 3)
        {
            Destroy(items[0]);
            items.RemoveAt(0);
        }
    }

    public void eat_item(string name)
    {
        if (GameSystem.instance.item_num[name] == 0) // 못먹었던 아이템이면
            GameSystem.instance.item_time.Add(name);
        GameSystem.instance.item_num[name] += 1;

        if (SceneManager.GetActiveScene().name == "Game_SceneNew")
        {
            if (Dictionarys.instance.dictionary_num[name] == false) // '한번도' 못먹었던 아이템이면 (도감용)
                Dictionarys.instance.dictionary_num[name] = true;
        }

        GameObject item = Instantiate(eat_box, new Vector3(0, 0, 0), Quaternion.identity, view.transform);
        item.GetComponent<Eat_box>().image.sprite = Resources.Load<Sprite>("Item2D/" + name);
        item.GetComponent<Eat_box>().text.text = GameSystem.instance.item_search(name, "name_ko");
        
        items.Add(item);
    }

    public void eat_remove()
    {
        items.RemoveAt(0);
    }
}

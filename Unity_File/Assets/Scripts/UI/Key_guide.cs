﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Key_guide : MonoBehaviour
{
    public static Key_guide instance; // 현재 클레스를 인스턴트화

    public GameObject talk;
    public GameObject item;
    public GameObject climb;
    public GameObject objects;
    public GameObject door;

    public GameObject item_name_ob;
    public Text item_name;

    public GameObject object_name_ob;
    public Text object_name;

    public static Key_guide Instance
    {
        get { return instance; }
    }
    void Start()
    {
        instance = this;
    }

    public void item_name_on(string name, Vector3 ts)
    {
        item_name_ob.SetActive(true);
        item_name_ob.transform.position = new Vector3(ts.x, ts.y + 70, ts.z);
        item_name.text = name;
    }
    public void item_name_off()
    {
        item_name_ob.SetActive(false);
    }

    public void talk_on()
    {
        talk.SetActive(true);
    }
    public void talk_off()
    {
        talk.SetActive(false);
    }

    public void item_on()
    {
        item.SetActive(true);
    }
    public void item_off()
    {
        item.SetActive(false);
    }

    public void climb_on()
    {
        climb.SetActive(true);
    }
    public void climb_off()
    {
        climb.SetActive(false);
    }

    public void door_on()
    {
        door.SetActive(true);
    }
    public void door_off()
    {
        door.SetActive(false);
    }

    public void object_on(string name, Vector3 ts)
    {
        object_name_ob.SetActive(true);
        object_name_ob.transform.position = new Vector3(ts.x, ts.y + 70, ts.z);
        object_name.text = name;

        objects.SetActive(true);
    }
    public void object_off()
    {
        object_name_ob.SetActive(false);
        objects.SetActive(false);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Qick_slot_sum : MonoBehaviour
{
    int click = 1;
    public string item_names;

    public GameObject image;

    public GameObject slot1;
    public GameObject slot2;
    public GameObject slot3;
    public GameObject slot4;
    public GameObject slot5;


    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) && InputManager.instance.click_mod == 0)
        {
            click = 1;
            image.transform.position = slot1.transform.position;
            item_names = slot1.GetComponent<Quick_slot>().item_name;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2) && InputManager.instance.click_mod == 0)
        {
            click = 2;
            image.transform.position = slot2.transform.position;
            item_names = slot2.GetComponent<Quick_slot>().item_name;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3) && InputManager.instance.click_mod == 0)
        {
            click = 3;
            image.transform.position = slot3.transform.position;
            item_names = slot3.GetComponent<Quick_slot>().item_name;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4) && InputManager.instance.click_mod == 0)
        {
            click = 4;
            image.transform.position = slot4.transform.position;
            item_names = slot4.GetComponent<Quick_slot>().item_name;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5) && InputManager.instance.click_mod == 0)
        {
            click = 5;
            image.transform.position = slot5.transform.position;
            item_names = slot5.GetComponent<Quick_slot>().item_name;
        }
    }
}

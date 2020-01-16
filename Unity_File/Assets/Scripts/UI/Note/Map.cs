﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Map : MonoBehaviour
{
    public Text TimePosition;

    string time_switch = null;
    int time_hour;
    int time_minute;

    public GameObject map_bg;
    Vector3 move_start; // 이동 할때 누르기 시작시 좌표
    Vector3 move_now; // 이동 할때 누르기 시작할때 이미지 좌표

    void Update()
    {
        // 시간 위치 표시
        Vector3 pos = Input.mousePosition;

        if(GameSystem.instance.time < 60 * 12)
        {
            time_switch = "AM";
            time_hour = (int)GameSystem.instance.time / 60;
            time_minute = (int)GameSystem.instance.time % 60;
        }
        else
        {
            time_switch = "PM";
            time_hour = ((int)GameSystem.instance.time - 60 * 12) / 60;
            time_minute = ((int)GameSystem.instance.time - 60 * 12) % 60;
        }

        TimePosition.text = "시간 " + time_switch + " " + time_hour.ToString() + ":" + time_minute.ToString() + "\n위치 (" + ((int)pos.x).ToString() + ", " + ((int)pos.y).ToString() + ")";


        // 지도 이동
        if (Input.GetMouseButtonDown(0))
        {
            move_start = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
            move_now = map_bg.transform.position;
        }
        else if(Input.GetMouseButton(0))
        {
            map_bg.transform.position = move_now - (move_start - new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
        }


        // 지도 확대 축소
        float wheelInput = Input.GetAxis("Mouse ScrollWheel");
        if (wheelInput > 0)
        {
            map_bg.transform.localScale = new Vector3(map_bg.transform.localScale.x + 0.1f, map_bg.transform.localScale.y + 0.1f, 1);
        }
        else if (wheelInput < 0)
        {
            map_bg.transform.localScale = new Vector3(map_bg.transform.localScale.x - 0.1f, map_bg.transform.localScale.y - 0.1f, 1);
        }
     }
}
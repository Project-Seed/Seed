using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Map : MonoBehaviour
{
    public Text TimePosition;

    string time_switch = null;
    int time_hour;
    int time_minute;

    void Start()
    {
        
    }

    void Update()
    {
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
    }
}

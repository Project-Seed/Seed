using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System;

using System.Linq;
using System.Xml.Serialization;
using System.Runtime.Serialization;
using System.Xml.Linq;
using System.Xml;


public class SaveData : MonoBehaviour
{
    public int num = 1; //몇번째 슬롯 인지

    public Text save;
    public Text time;

    public GameObject slider;
    public Slider hp;
    public Slider radiation;

    public SaveData_content saveData_Content;

    public GameObject choose;


    private void OnEnable()
    {
        open();
    }

    public void open()
    {
        string strFile = "./Assets/save_data" + num.ToString() + ".xml";
        FileInfo fileInfo = new FileInfo(strFile); // 파일 있는지 체크

        if (fileInfo.Exists)
        {
            XDocument save_data = XDocument.Load("./Assets/save_data" + num.ToString() + ".xml");

            save.text = GameSystem.instance.map_name;
            time.text = save_data.Element("root").Element("solo").Element("save_time").Value;

            slider.SetActive(true);
            hp.value = float.Parse(save_data.Element("root").Element("solo").Element("hp").Value);
            hp.maxValue = PlayerState.instance.max_hp;
            radiation.value = float.Parse(save_data.Element("root").Element("solo").Element("radiation").Value);
            radiation.maxValue = PlayerState.instance.max_radiation;
        }
        else
        {
            save.text = "Empty Slot";
            time.text = "";

            slider.SetActive(false);
        }
    }

    public void click()
    {
        saveData_Content.click();
        choose.SetActive(true);

        string strFile = "./Assets/save_data" + num.ToString() + ".xml";
        FileInfo fileInfo = new FileInfo(strFile); // 파일 있는지 체크

        if (fileInfo.Exists)
        {
            XDocument save_data = XDocument.Load("./Assets/save_data" + num.ToString() + ".xml");

            saveData_Content.save.text = save.text;
            saveData_Content.time.text = time.text;

            string a = "";
            foreach (var load in save_data.Element("root").Element("quest_state").Elements())
            {
                if (Convert.ToInt32(load.Value) == 1 || Convert.ToInt32(load.Value) == 2)
                    a += GameSystem.instance.quest_list[Convert.ToInt32(load.Name.LocalName.Substring(4)) - 1]["title"] + "\n"; 
            }

            saveData_Content.quest.text = a;

            saveData_Content.slider.SetActive(true);
            saveData_Content.hp.value = hp.value;
            saveData_Content.hp.maxValue = hp.maxValue;
            saveData_Content.radiation.value = radiation.value;
            saveData_Content.radiation.maxValue = radiation.maxValue;
        }
        else
        {
            saveData_Content.save.text = save.text;
            saveData_Content.time.text = time.text;
            saveData_Content.quest.text = "";

            saveData_Content.slider.SetActive(false);
        }

        saveData_Content.click_num = num;
    }
}

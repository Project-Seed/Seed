using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Quest : MonoBehaviour
{
    public GameObject view_main;
    public GameObject view_sub;

    public GameObject quest_data; // 프리팹
    public List<GameObject> quest_data_main; // 프리팹으로 만든 퀘스트 목록
    public List<GameObject> quest_data_sub; // 프리팹으로 만든 퀘스트 목록2

    public Text data_title;
    public Text data_area;
    public Text data_npc;
    public Text data_content;

    public List<GameObject> category_ob;
    public List<Sprite> ox_sp;

    private void OnEnable()
    {
        data_title.text = null;
        data_area.text = null;
        data_npc.text = null;
        data_content.text = null;

        for(int i=0; i< quest_data_main.Count; i++)
        {
            Destroy(quest_data_main[i]);
        }
        quest_data_main.Clear();
        for (int i = 0; i < quest_data_sub.Count; i++)
        {
            Destroy(quest_data_sub[i]);
        }
        quest_data_sub.Clear();

        for(int i= GameSystem.instance.quest_list.Count-1; i>=0; i--)
        {
            if(GameSystem.instance.quest_state[i+1] != 0 && GameSystem.instance.quest_state[i + 1] != 3)
            {
                if(GameSystem.instance.quest_list[i]["category"] == "main")
                {
                    GameObject add_data = Instantiate(quest_data, view_main.transform); 
                    add_data.GetComponent<Quest_Data>().quest_num = i + 1;
                    add_data.GetComponent<Quest_Data>().quest_ob = gameObject;
                    add_data.GetComponent<Quest_Data>().ox.sprite = ox_sp[1];
                    add_data.GetComponent<Quest_Data>().title.text = GameSystem.instance.quest_list[i]["title"];
                    add_data.GetComponent<Quest_Data>().area.text = GameSystem.instance.quest_list[i]["area"];
                    quest_data_main.Add(add_data);
                }
                else
                {
                    GameObject add_data = Instantiate(quest_data, view_sub.transform);
                    add_data.GetComponent<Quest_Data>().quest_num = i + 1;
                    add_data.GetComponent<Quest_Data>().quest_ob = gameObject;
                    add_data.GetComponent<Quest_Data>().ox.sprite = ox_sp[1];
                    add_data.GetComponent<Quest_Data>().title.text = GameSystem.instance.quest_list[i]["title"];
                    add_data.GetComponent<Quest_Data>().area.text = GameSystem.instance.quest_list[i]["area"];
                    quest_data_main.Add(add_data);
                }
            }
            else if (GameSystem.instance.quest_state[i + 1] == 3)
            {
                if (GameSystem.instance.quest_list[i]["category"] == "main")
                {
                    GameObject add_data = Instantiate(quest_data, view_main.transform);
                    add_data.GetComponent<Quest_Data>().quest_num = i + 1;
                    add_data.GetComponent<Quest_Data>().quest_ob = gameObject;
                    add_data.GetComponent<Quest_Data>().ox.sprite = ox_sp[0];
                    add_data.GetComponent<Quest_Data>().title.text = GameSystem.instance.quest_list[i]["title"];
                    add_data.GetComponent<Quest_Data>().area.text = GameSystem.instance.quest_list[i]["area"];
                    quest_data_main.Add(add_data);
                }
                else
                {
                    GameObject add_data = Instantiate(quest_data, view_sub.transform);
                    add_data.GetComponent<Quest_Data>().quest_num = i + 1;
                    add_data.GetComponent<Quest_Data>().quest_ob = gameObject;
                    add_data.GetComponent<Quest_Data>().ox.sprite = ox_sp[0];
                    add_data.GetComponent<Quest_Data>().title.text = GameSystem.instance.quest_list[i]["title"];
                    add_data.GetComponent<Quest_Data>().area.text = GameSystem.instance.quest_list[i]["area"];
                    quest_data_main.Add(add_data);
                }
            }
        }
    }

    public void main_button()
    {
        category_ob[0].SetActive(true);
        category_ob[1].SetActive(false);

        view_main.SetActive(true);
        view_sub.SetActive(false);
    }

    public void sub_button()
    {
        category_ob[0].SetActive(false);
        category_ob[1].SetActive(true);

        view_main.SetActive(false);
        view_sub.SetActive(true);
    }

    public void data_button(int num, GameObject a)
    {
        for (int i = 0; i < quest_data_main.Count; i++)
        {
            quest_data_main[i].GetComponent<Quest_Data>().choose.SetActive(false);
        }
        for (int i = 0; i < quest_data_sub.Count; i++)
        {
            quest_data_sub[i].GetComponent<Quest_Data>().choose.SetActive(false);
        }
        a.GetComponent<Quest_Data>().choose.SetActive(true);

        data_title.text = GameSystem.instance.quest_list[num - 1]["title"];
        data_area.text = GameSystem.instance.quest_list[num - 1]["area"];
        data_npc.text = GameSystem.instance.quest_list[num - 1]["name"];
        data_content.text = GameSystem.instance.quest_list[num - 1]["content"] + "\n" +
            Quest_clear_system.instance.num_data(num);
    }
}

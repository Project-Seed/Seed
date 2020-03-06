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

    int now_click = 0; // 0이면 메인퀘스트, 1이면 서브퀘스트

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

        for(int i=0; i<GameSystem.instance.quest_list.Count; i++)
        {
            if(GameSystem.instance.quest_state[i+1] != 0 && GameSystem.instance.quest_state[i + 1] != 3)
            {
                if(GameSystem.instance.quest_list[i]["category"] == "main")
                {
                    GameObject add_data = Instantiate(quest_data, view_main.transform); 
                    add_data.GetComponent<Quest_Data>().quest_num = i + 1;
                    add_data.GetComponent<Quest_Data>().quest_ob = gameObject;
                    add_data.GetComponent<Quest_Data>().image.color = new Color(1, 0.9994238f, 0.5801887f, 1);
                    add_data.GetComponent<Quest_Data>().title.text = GameSystem.instance.quest_list[i]["title"];
                    add_data.GetComponent<Quest_Data>().area.text = GameSystem.instance.quest_list[i]["area"];
                    quest_data_main.Add(add_data);
                }
                else
                {
                    GameObject add_data = Instantiate(quest_data, view_main.transform);
                    add_data.GetComponent<Quest_Data>().quest_num = i + 1;
                    add_data.GetComponent<Quest_Data>().quest_ob = gameObject;
                    add_data.GetComponent<Quest_Data>().image.color = new Color(1, 0.9994238f, 0.5801887f, 1);
                    add_data.GetComponent<Quest_Data>().title.text = GameSystem.instance.quest_list[i]["title"];
                    add_data.GetComponent<Quest_Data>().area.text = GameSystem.instance.quest_list[i]["area"];
                    quest_data_main.Add(add_data);
                }
            }
            else if (GameSystem.instance.quest_state[i + 1] == 3)
            {
                if (GameSystem.instance.quest_list[i]["category"] == "main")
                {
                    GameObject add_data = Instantiate(quest_data, view_sub.transform);
                    add_data.GetComponent<Quest_Data>().quest_num = i + 1;
                    add_data.GetComponent<Quest_Data>().quest_ob = gameObject;
                    add_data.GetComponent<Quest_Data>().image.color = new Color(0.8396226f, 0.8396226f, 0.8396226f, 1);
                    add_data.GetComponent<Quest_Data>().title.text = GameSystem.instance.quest_list[i]["title"];
                    add_data.GetComponent<Quest_Data>().area.text = GameSystem.instance.quest_list[i]["area"];
                    quest_data_main.Add(add_data);
                }
                else
                {
                    GameObject add_data = Instantiate(quest_data, view_sub.transform);
                    add_data.GetComponent<Quest_Data>().quest_num = i + 1;
                    add_data.GetComponent<Quest_Data>().quest_ob = gameObject;
                    add_data.GetComponent<Quest_Data>().image.color = new Color(0.8396226f, 0.8396226f, 0.8396226f, 1);
                    add_data.GetComponent<Quest_Data>().title.text = GameSystem.instance.quest_list[i]["title"];
                    add_data.GetComponent<Quest_Data>().area.text = GameSystem.instance.quest_list[i]["area"];
                    quest_data_main.Add(add_data);
                }
            }
        }
    }

    public void main_button()
    {
        now_click = 0;
        view_main.SetActive(true);
        view_sub.SetActive(false);
    }

    public void sub_button()
    {
        now_click = 1;
        view_main.SetActive(false);
        view_sub.SetActive(true);
    }

    public void data_button(int num)
    {
        data_title.text = GameSystem.instance.quest_list[num - 1]["title"];
        data_area.text = GameSystem.instance.quest_list[num - 1]["area"];
        data_npc.text = GameSystem.instance.quest_list[num - 1]["name"];
        data_content.text = GameSystem.instance.quest_list[num - 1]["content"];
    }
}

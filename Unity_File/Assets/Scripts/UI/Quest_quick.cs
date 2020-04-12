using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest_quick : MonoBehaviour
{
    public List<GameObject> quest_data; // 프리팹으로 만든 퀘스트 목록
    public GameObject quest_quick_data;
    public GameObject view;

    public void quest_re()
    {
        for (int i = 0; i < quest_data.Count; i++)
        {
            Destroy(quest_data[i]);
        }
        quest_data.Clear();

        for (int i = 0; i < GameSystem.instance.quest_list.Count; i++)
        {
            if (GameSystem.instance.quest_state[i + 1] != 0 && GameSystem.instance.quest_state[i + 1] != 3)
            {
                GameObject add_data = Instantiate(quest_quick_data, view.transform);
                add_data.GetComponent<Quest_quick_data>().setting(i + 1);

                quest_data.Add(add_data);
            }
        }
    }
}

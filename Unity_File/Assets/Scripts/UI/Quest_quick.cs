using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Quest_quick : MonoBehaviour
{
    public List<GameObject> quest_data; // 프리팹으로 만든 퀘스트 목록
    public GameObject quest_quick_data;
    public GameObject view;

    public GameObject active;
    public Text quest_text;
    public Image clear_text;

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

    public void start_co(string name, bool clear_on)
    {
        StopAllCoroutines();
        StartCoroutine(active_on(name, clear_on));
    }

    public IEnumerator active_on(string name, bool clear_on)
    {
        GameSystem.instance.sound_start(8);

        active.SetActive(true);
        quest_text.text = name;
        quest_text.color = new Color(0, 0, 0, 0);
        clear_text.color = new Color(1, 0.9701258f, 0.7311321f, 0);

        for (int i = 0; i <= 100; i++)
        {
            if (i <= 15)
            {
                active.GetComponent<RectTransform>().anchoredPosition = new Vector2(0f, 250f);
                active.GetComponent<RectTransform>().pivot = new Vector2(1f, 0.5f);

                active.GetComponent<RectTransform>().sizeDelta = new Vector2(i / 15f * 1920, 120);
                quest_text.color = new Color(0, 0, 0, i / 15f);

                if(clear_on)
                    clear_text.color = new Color(1, 0.9701258f, 0.7311321f, i / 15f);
                else
                    clear_text.color = new Color(1, 0.9701258f, 0.7311321f, 0);
            }
            else if(i >= 85)
            {
                active.GetComponent<RectTransform>().pivot = new Vector2(0f, 0.5f);
                active.GetComponent<RectTransform>().anchoredPosition = new Vector2(-1920f, 250f);

                active.GetComponent<RectTransform>().sizeDelta = new Vector2((100 - i) / 15f * 1920, 120);
                quest_text.color = new Color(0, 0, 0, (100 - i) / 15f);

                if (clear_on)
                    clear_text.color = new Color(1, 0.9701258f, 0.7311321f, (100 - i) / 15f);
                else
                    clear_text.color = new Color(1, 0.9701258f, 0.7311321f, 0);
            }
            yield return new WaitForSeconds(0.01f);
        }
        active.SetActive(false);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class New_world : MonoBehaviour
{
    string now_map = "";

    public Image me;
    public GameObject line;
    public Text text;

    private void Awake()
    {
        me.color = new Color(1, 1, 1, 0);
        line.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 3);
        text.color = new Color(1, 1, 1, 0);
    }

    private void Update()
    { 
        if(now_map != GameSystem.instance.map_name)
        {
            StopCoroutine(view());
            StartCoroutine(view());

            for(int i=0; i<GameSystem.instance.world_list.Count;i++)
            {
                if(GameSystem.instance.world_list[i]["name"] == GameSystem.instance.map_name)
                {
                    text.text = GameSystem.instance.world_list[i]["name_ko"];
                    PlayerState.instance.radiation_level = int.Parse(GameSystem.instance.world_list[i]["level"]);

                    i = GameSystem.instance.world_list.Count;
                }
            }

            now_map = GameSystem.instance.map_name;
        }
    }

    IEnumerator view()
    {
        me.color = new Color(1, 1, 1, 0);
        line.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 3);
        text.color = new Color(1, 1, 1, 0);

        for (int i = 0; i <= 200; i++)
        {
            if (i <= 20)
                me.color = new Color(1, 1, 1, i / 20f);
            else if (i <= 50)
            {
                line.GetComponent<RectTransform>().sizeDelta = new Vector2((i - 20) / 30f * 622f, 3);
                text.color = new Color(1, 1, 1, (i - 20) / 30f);
            }
            else if (i >= 170)
            {
                me.color = new Color(1, 1, 1, (200 - i) / 30f);
                line.GetComponent<RectTransform>().sizeDelta = new Vector2((200 - i) / 30f * 622f, 3);
                text.color = new Color(1, 1, 1, (200 - i) / 30f);
            }

            yield return new WaitForSeconds(0.01f);
        }
    }
}

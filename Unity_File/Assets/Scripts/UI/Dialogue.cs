using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dialogue : MonoBehaviour
{
    public GameObject npc_name;
    public GameObject dialogue_box;

    public Text name_text;
    public GameObject quest_state;
    public Image quest_image;

    public Sprite quest_start;
    public Sprite quest_ing;
    public Sprite quest_end;

    public GameObject quest_quick; // 퀘스트 퀵슬롯 갱신용

    bool quest_bool = false;

    bool dialogue_box_bool = false;

    public Camera cameras;
    public GameObject npc_ob;
    GameObject name_position;

    int onss = 0; // 1이면 퀘스트 있음

    int quest_num; // 충돌 npc의 퀘스트 번호
    int quest_now; // 충돌 npc의 퀘스트 진행상태

    public bool open_on = false; // 외부에서 대화 불러서 했으면 변화 없어서 에러 해결
    

    void Update()
    {
        if (quest_bool == true)
        {
            Vector3 guide_pos = cameras.WorldToScreenPoint(npc_ob.transform.position);
            Vector3 name_pos = cameras.WorldToScreenPoint(name_position.transform.position);

            npc_name.transform.position = new Vector3(name_pos.x, name_pos.y + 20, npc_name.transform.position.z);

            if (Input.GetKeyDown(KeyCode.H) && InputManager.instance.click_mod == 0 && onss == 1)
            {
                if (dialogue_box_bool == false)
                {
                    StartCoroutine(Key_guide.instance.talk_ing());
                    InputManager.instance.game_stop();

                    dialogue_box.SetActive(true);
                    dialogue_box_bool = true;

                    if(quest_now == 0)
                        gameObject.GetComponent<Text_system>().StartDialogue(System.Convert.ToInt32(GameSystem.instance.quest_list[quest_num - 1]["start_talk"]));
                    else if (quest_now == 1)
                        gameObject.GetComponent<Text_system>().StartDialogue(System.Convert.ToInt32(GameSystem.instance.quest_list[quest_num - 1]["ing_talk"]));
                    else if(quest_now == 2)
                        gameObject.GetComponent<Text_system>().StartDialogue(System.Convert.ToInt32(GameSystem.instance.quest_list[quest_num - 1]["end_talk"]));
                }
            }
        }

        if (dialogue_box.activeSelf)
            GameSystem.instance.talk_trigger = true;
        else
            GameSystem.instance.talk_trigger = false;
    }

    public void quest_on(GameObject npc_obs, GameObject name_positions, string name)
    {
        onss = 0;

        if (open_on == true)
        {
            npc_name.SetActive(true);
            Key_guide.instance.talk_on();

            npc_ob = npc_obs;
            name_position = name_positions;
            name_text.text = name;

            GameSystem.instance.talk_npc_ob = npc_ob;

            quest_bool = true;
        }

        for (int i=0; i<GameSystem.instance.quest_list.Count; i++)
        {
            if(GameSystem.instance.quest_list[i]["name"] == name && // npc이름이 같고
                GameSystem.instance.quest_state[i+1] != 3 && // 클리어 안된 퀘스트 이고
                (GameSystem.instance.quest_list[i]["preceding"] == "0" || GameSystem.instance.quest_state[System.Convert.ToInt32(GameSystem.instance.quest_list[i]["preceding"])] == 3)) // 선행조건이 클리어 된경우
            {
                quest_num = i + 1;
                quest_now = GameSystem.instance.quest_state[quest_num];
                onss = 1;

                switch (quest_now)
                {
                    case 0:
                        quest_image.sprite = quest_start;
                        break;

                    case 1:
                        quest_image.sprite = quest_ing;
                        break;

                    case 2:
                        quest_image.sprite = quest_end;
                        break;
                }
                break;
            }
        }

        if(onss != 1)
        {
            Key_guide.instance.talk_off();
            quest_image.color = new Color(0, 0, 0, 0);
        }
        else
        {
            quest_image.color = new Color(1, 1, 1, 1);
        }
    }

    public void quest_off()
    {
        npc_name.SetActive(false);
        Key_guide.instance.talk_off();

        quest_bool = false;
    }

    public void talk_end()
    {
        InputManager.instance.game_start();

        dialogue_box.SetActive(false);
        dialogue_box_bool = false;


        StartCoroutine(res()); //퀘스트 갱신때매 잠시 딜레이
        quest_quick.GetComponent<Quest_quick>().quest_re(); // 퀘스트 퀵 갱신
    }

    IEnumerator res()
    {
        yield return new WaitForSeconds(0.1f);

        quest_on(npc_ob, name_position, name_text.text);
    }

    public void solo_talk(int num)
    {
        onss = 1;

        InputManager.instance.game_stop();

        dialogue_box.SetActive(true);
        dialogue_box_bool = true;

        gameObject.GetComponent<Text_system>().StartDialogue(num);
    }
}

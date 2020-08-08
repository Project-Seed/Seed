using System.Collections;
using System.Collections.Generic;
using System.Security.Permissions;
using UnityEngine;

public class Quest_clear_system : MonoBehaviour
{
    public static Quest_clear_system instance;

    public Dictionary<int, int> clear_trigger = new Dictionary<int, int>();

    public static Quest_clear_system Instance
    {
        get { return instance; }
    }

    private void Awake()
    {
        instance = this;

        for (int i = 1; i <= 21; i++) // 퀘스트 갯수 많큼 늘려주세요
            clear_trigger.Add(i, 0);
    }

    void Update()
    {
        //state를 3으로 하여 퀘스트를 끝낼경우 GameObject.Find("Quest_quick").GetComponent<Quest_quick>().quest_re(); 필수!!!

        if (GameSystem.instance.quest_state[1] == 1)
            GameSystem.instance.quest_state[1] = 2;

        if (GameSystem.instance.quest_state[2] == 1)
            GameSystem.instance.quest_state[2] = 2;

        if (GameSystem.instance.quest_state[3] == 1)
            GameSystem.instance.quest_state[3] = 2;

        if (GameSystem.instance.quest_state[4] == 1 &&
            GameSystem.instance.item_num["coal"] >= 5)
            GameSystem.instance.quest_state[4] = 2;

        state_changer(6, 4);
        state_changer(7, 2);
        state_changer(8, 1);
        state_changer(9, 1);
        state_changer(10, 1);
        state_changer(11, 1);

        if (GameSystem.instance.quest_state[12] == 2)
        {
            GameSystem.instance.quest_state[12] = 3;
            clear_reward(12);
            GameObject.Find("Quest_quick").GetComponent<Quest_quick>().start_co(GameSystem.instance.quest_list[12 - 1]["title"], true);
            GameObject.Find("Quest_quick").GetComponent<Quest_quick>().quest_re();
        }

        state_changer(13, 1);

        if (GameSystem.instance.quest_state[14] == 1 &&
            GameSystem.instance.item_num["sticky_moss"] >= 1)
            GameSystem.instance.quest_state[14] = 2;

        if (GameSystem.instance.quest_state[15] == 2)
        {
            GameSystem.instance.quest_state[15] = 3;
            clear_reward(15);
            GameObject.Find("Quest_quick").GetComponent<Quest_quick>().start_co(GameSystem.instance.quest_list[15 - 1]["title"], true);
            GameObject.Find("Quest_quick").GetComponent<Quest_quick>().quest_re();
        }

        if(GameSystem.instance.quest_state[16] == 1)
        {
            GameSystem.instance.quest_state[16] = 3;
            clear_reward(16);
            GameObject.Find("Quest_quick").GetComponent<Quest_quick>().start_co(GameSystem.instance.quest_list[16 - 1]["title"], true);
            GameObject.Find("Quest_quick").GetComponent<Quest_quick>().quest_re();
        }

        if (GameSystem.instance.quest_state[17] == 1 &&
            GameSystem.instance.item_num["coal"] >= 2)
            GameSystem.instance.quest_state[17] = 2;

        state_changer(18, 1);

        if (GameSystem.instance.quest_state[19] == 1 &&
            GameSystem.instance.item_num["fix_box"] >= 1)
            GameSystem.instance.quest_state[19] = 2;

        if (GameSystem.instance.quest_state[5] == 2)
        {
            GameSystem.instance.quest_state[5] = 3;
            clear_reward(5);
            GameObject.Find("Quest_quick").GetComponent<Quest_quick>().start_co(GameSystem.instance.quest_list[5 - 1]["title"], true);
            GameObject.Find("Quest_quick").GetComponent<Quest_quick>().quest_re();
        }

        if (GameSystem.instance.quest_state[20] == 1)
        {
            GameSystem.instance.quest_state[20] = 3;
            clear_reward(20);
            GameObject.Find("Quest_quick").GetComponent<Quest_quick>().start_co(GameSystem.instance.quest_list[20 - 1]["title"], true);
            GameObject.Find("Quest_quick").GetComponent<Quest_quick>().quest_re();
        }

        if (GameSystem.instance.quest_state[21] == 1)
        {
            GameSystem.instance.quest_state[21] = 3;
            clear_reward(21);
            GameObject.Find("Quest_quick").GetComponent<Quest_quick>().start_co(GameSystem.instance.quest_list[21 - 1]["title"], true);
            GameObject.Find("Quest_quick").GetComponent<Quest_quick>().quest_re();
        }
    }

    void state_changer(int quest_num, int item_num)
    {
        if (GameSystem.instance.quest_state[quest_num] == 1 &&
                    clear_trigger[quest_num] == item_num)
        {
            GameSystem.instance.quest_state[quest_num] = 3;
            clear_reward(quest_num);
            GameObject.Find("Quest_quick").GetComponent<Quest_quick>().start_co(GameSystem.instance.quest_list[quest_num - 1]["title"], true);
            GameObject.Find("Quest_quick").GetComponent<Quest_quick>().quest_re();
        }
    }

    public string num_data(int num)
    {
        string data = null;

        switch(num)
        {
            case 1:
            case 2:
            case 3:
                data = "잠시 후 다시 말을 걸어보자";
                break;
            case 4:
                data = "석탄 " + GameSystem.instance.item_num["coal"].ToString() + " / 5";
                break;
            case 6:
                data = "라디오, 가족사진, 편지, 다이어리 찾기 " + clear_trigger[6] + " / 4";
                break;
            case 7:
                data = "해리포터 4권 찾기";
                break;
            case 8:
                data = "식물 책 조사하기";
                break;
            case 9:
                data = "지하실 가보기";
                break;
            case 10:
                data = "지하실 조사하기";
                break;
            case 11:
                data = "집 밖으로 나가기";
                break;
            case 12:
                data = "마을주민 찾아가기";
                break;
            case 13:
                data = "돌을 넘어 가기";
                break;
            case 14:
                data = "끈적이는 이끼 " + GameSystem.instance.item_num["sticky_moss"].ToString() + " / 1";
                break;
            case 15:
                data = "홉스 아저씨와 대화하기";
                break;
            case 16:
                data = "동굴 조사하기";
                break;
            case 17:
                data = "석탄 " + GameSystem.instance.item_num["coal"].ToString() + " / 2";
                break;
            case 18:
                data = "방해물 제거하기";
                break;
            case 19:
                data = "수리상자 " + GameSystem.instance.item_num["fix_box"].ToString() + " / 1";
                break;
            case 20:
            case 21:
                data = "";
                break;
        }

        return data;
    }

    public void clear_reward(int num)
    {
        switch (num)
        {
            case 1:
            case 2:
            case 3:
            case 4:
                Eat_system.instance.eat_item("aliquot_part");
                break;
            case 6:
            case 7:
            case 8:
            case 9:
            case 10:
            case 11:
            case 12:
            case 18:
                break;
            case 13:
            case 15:
            case 16:
            case 5:
                Eat_system.instance.eat_item("aliquot_part");
                break;
            case 14:
                Eat_system.instance.eat_item("aliquot_part");
                GameObject.Find("BridgeTimeline").GetComponent<TimelineController>().Play();
                break;
            case 17:
                Eat_system.instance.eat_item("gunpowder");
                Eat_system.instance.eat_item("gunpowder");
                Eat_system.instance.eat_item("gunpowder");
                break;
            case 19:
                // 수리 전경
                break;
            case 20:
                // 트루엔딩
                Debug.Log("트루엔딩"); Eat_system.instance.eat_item("gunpowder");
                break;
            case 21:
                // 배드엔딩
                Debug.Log("배드엔딩"); Eat_system.instance.eat_item("coal");
                break;
        }
    }
}

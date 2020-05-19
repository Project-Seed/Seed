using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest_clear_system : MonoBehaviour
{
    public static Quest_clear_system instance;

    public static Quest_clear_system Instance
    {
        get { return instance; }
    }

    private void Awake()
    {
        instance = this;
    }

    void Update()
    {
        if(GameSystem.instance.quest_state[1] == 1)
            GameSystem.instance.quest_state[1] = 2;

        if (GameSystem.instance.quest_state[2] == 1)
            GameSystem.instance.quest_state[2] = 2;

        if (GameSystem.instance.quest_state[3] == 1)
            GameSystem.instance.quest_state[3] = 2;

        if (GameSystem.instance.quest_state[4] == 1 &&
            GameSystem.instance.item_num["coal"] >= 5)
            GameSystem.instance.quest_state[4] = 2;

        if (GameSystem.instance.quest_state[5] == 1 &&
            GameSystem.instance.item_num["coal"] >= 1)
            GameSystem.instance.quest_state[5] = 2;
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
                data = "석탄" + GameSystem.instance.item_num["coal"].ToString() + " / 5";
                break;

            case 5:
                data = "끈적이는 이끼" + GameSystem.instance.item_num["sticky_moss"].ToString() + " / 1";
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
            case 5:
                Eat_system.instance.eat_item("aliquot_part");
                break;
        }
    }
}

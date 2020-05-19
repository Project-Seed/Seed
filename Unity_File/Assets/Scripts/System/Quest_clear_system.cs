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
        }

        return data;
    }
}

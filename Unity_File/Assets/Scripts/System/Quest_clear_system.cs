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
        if(GameSystem.instance.quest_state[1] == 1 &&
            GameSystem.instance.item_num["acacia"] >= 1)
            GameSystem.instance.quest_state[1] = 2;
    }

    public string num_data(int num)
    {
        string data = null;

        switch(num)
        {
            case 1:
                data = "아카시아꽃 " + GameSystem.instance.item_num["acacia"].ToString() + "/1";
                break;
        }

        Debug.Log(data);
        return data;
    }
}

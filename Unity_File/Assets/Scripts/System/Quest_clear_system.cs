using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest_clear_system : MonoBehaviour
{
    void Update()
    {
        if(GameSystem.instance.quest_state[1] == 1 &&
            GameSystem.instance.item_num["acacia"] >= 1)
            GameSystem.instance.quest_state[1] = 2;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Quest_quick_data : MonoBehaviour
{
    public Text title;
    public Text content;

    bool starts = false;
    int nums;

    private void Update()
    {
        if(starts)
        {
            content.text = Quest_clear_system.instance.num_data(nums);
        }
    }

    public void setting(int num)
    {
        nums = num;
        starts = true;

        title.text = GameSystem.instance.quest_list[nums - 1]["title"];
    }
}

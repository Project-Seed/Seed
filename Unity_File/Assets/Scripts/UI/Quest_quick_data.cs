using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Quest_quick_data : MonoBehaviour
{
    public Text title;
    public Text content;

    public GameObject o;
    public GameObject x;

    bool starts = false;
    int nums;

    private void Update()
    {
        if(starts)
        {
            content.text = Quest_clear_system.instance.num_data(nums);

            if (GameSystem.instance.quest_state[nums] == 2)
            {
                o.SetActive(true);
                x.SetActive(false);
            }
            else
            {
                o.SetActive(false);
                x.SetActive(true);
            }
        }
    }

    public void setting(int num)
    {
        nums = num;
        starts = true;

        title.text = GameSystem.instance.quest_list[nums - 1]["title"];
    }
}

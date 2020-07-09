using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tutorial_ob : MonoBehaviour
{
    public Dialogue dialogue;
    public int die_num = 0; // 1이면 특수

    private void Start()
    {
        dialogue = GameObject.Find("Dialogue").GetComponent<Dialogue>();
    }

    public void click(int num)
    {
        switch(num)
        {
            case 0:
                InputManager.instance.click_mod = 0;
                Destroy(gameObject);
                break;

            case 1:
                Instantiate(Resources.Load<GameObject>("Tutorial/Inven_check"), GameObject.Find("Canvas").transform);
                Destroy(gameObject);
                break;

            case 2:
                if (GameSystem.instance.quest_state[9] == 0)
                    dialogue.solo_talk(24);
                else
                    InputManager.instance.click_mod = 0;
                Destroy(gameObject);
                break;
        }
    }

    private void Update()
    {
        if(die_num == 1)
        {
            if (GameSystem.instance.quest_state[11] != 0)
                Destroy(gameObject);
        }
    }
}

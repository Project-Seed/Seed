using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest_trigger : MonoBehaviour
{
    public int num;

    private Dialogue dialogue;

    private void Start()
    {
        dialogue = GameObject.Find("Dialogue").GetComponent<Dialogue>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            switch (num)
            {
                case 1:
                    if (GameSystem.instance.quest_state[12] == 0)
                    {
                        GameSystem.instance.quest_state[5] = 1;
                        GameObject.Find("Quest_quick").GetComponent<Quest_quick>().quest_re();
                        dialogue.solo_talk(29);
                    }
                    break;

                case 2:
                    if (GameSystem.instance.quest_state[13] == 0)
                        dialogue.solo_talk(30);
                    break;

                case 3:
                    if (GameSystem.instance.quest_state[13] == 1)
                        Quest_clear_system.instance.clear_trigger[13] = 1;
                    break;

                case 4:
                    PlayerState.instance.sleep_on();
                    break;

                case 5:
                    if (GameSystem.instance.quest_state[16] == 0)
                        dialogue.solo_talk(37);
                    else if (GameSystem.instance.quest_state[17] != 0 && GameSystem.instance.quest_state[18] == 0)
                        dialogue.solo_talk(41);
                    break;

                case 6:
                    if (GameSystem.instance.quest_state[18] == 1)
                        Quest_clear_system.instance.clear_trigger[18] = 1;
                    break;
            }
        }
    }
}

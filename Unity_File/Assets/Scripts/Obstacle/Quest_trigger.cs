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
                        dialogue.solo_talk(29);
                    break;

                case 2:
                    if (GameSystem.instance.quest_state[13] == 0)
                    {
                        Eat_system.instance.eat_item("blue_seed");
                        Eat_system.instance.eat_item("brown_seed");
                        Eat_system.instance.eat_item("brown_seed");
                        dialogue.solo_talk(30);
                    }
                    break;

                case 3:
                    if (GameSystem.instance.quest_state[13] == 1)
                        Quest_clear_system.instance.clear_trigger[13] = 1;
                    break;

            }
        }
    }
}

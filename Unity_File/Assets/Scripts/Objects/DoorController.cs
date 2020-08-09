using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    // 내가 보이니 //
    public Animator open;
    private Dialogue dialog;

    public void Start()
    {
        dialog = GameObject.Find("Dialogue").GetComponent<Dialogue>();
    }


    public void OpenDoor(string doorName)
    {
        if (!CheckDoor(doorName)) return;

        if (!open.GetBool("isOpen"))
        {
            open.SetBool("isOpen", true);

            GameSystem.instance.sound_start(12);
        }
    }

    private bool CheckDoor(string doorName)
    {
        if (doorName.Equals("DownDoor"))
        {
            if (GameSystem.instance.quest_state[9] >= 1)
                return true;
            else
            {
                dialog.solo_talk(27);
                return false;
            }
        }
        else if (doorName.Equals("FrontDoor"))
        {
            if (GameSystem.instance.quest_state[11] >= 1)
            {
                StartCoroutine(GameSystem.instance.TutorialEnd());
                return true;
            }
            else
            {
                dialog.solo_talk(27);
                return false;
            }
        }
        else return true;
    }

    public void CloseDoor()
    {
        if (open.GetBool("isOpen"))
        {
            open.SetBool("isOpen", false);
        }
    }

    public void KeyAppear()
    {
        Key_guide.instance.door_on();
    }

    public void KeyDisAppear()
    {
        Key_guide.instance.door_off();
    }

}

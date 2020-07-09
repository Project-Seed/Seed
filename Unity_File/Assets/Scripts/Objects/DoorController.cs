using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    // 내가 보이니 //
    public Animator open;

    public void OpenDoor()
    {
        if (!open.GetBool("isOpen"))
        {
            open.SetBool("isOpen", true);
        }
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tutorial_ob : MonoBehaviour
{
    public Dialogue dialogue;

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
                dialogue.solo_talk(24);
                Destroy(gameObject);
                break;
        }
    }
}

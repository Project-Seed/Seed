using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item_text : MonoBehaviour
{
    public Text text;

    public void text_reset(string name)
    {
        switch(name)
        {
            case "Box":
                text.text = "[F] : 박스";
                break;
        }
    }
}

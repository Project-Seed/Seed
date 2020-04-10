using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hetorigoto_text : MonoBehaviour
{
    public Text text;

    public void text_reset(string texts)
    {
        text.text = texts;
    }
}

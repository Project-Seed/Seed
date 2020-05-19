using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Quest_Data : MonoBehaviour
{
    public Text title;
    public Text area;

    public Image image;

    public int quest_num;

    public GameObject quest_ob;

    public Image ox;

    public void Button()
    {
        quest_ob.GetComponent<Quest>().data_button(quest_num);
    }
}

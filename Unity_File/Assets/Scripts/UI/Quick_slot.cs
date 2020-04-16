using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Quick_slot : MonoBehaviour
{
    bool actives = false;
    public string item_name = null;
    int num;

    public Text num_text;
    public Image item_image;

    public Inventory inven;

    void Update()
    {
        if(actives)
        {
            num = GameSystem.instance.item_num[item_name];
            num_text.text = num.ToString();
        }
    }

    public void click()
    {
        if (inven.quick_mod == true)
        {
            actives = true;
            item_name = inven.quick_name;
            item_image.sprite = Resources.Load<Sprite>("Item2D/" + item_name);

            inven.quick_off();
        }
    }
}

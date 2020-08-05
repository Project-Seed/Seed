using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class button_click : MonoBehaviour
{
    public Image image;

    public Sprite normal;
    public Sprite up;
    public Sprite click;

    bool up_true = false;
    public bool click_on = false;

    void Update()
    {
        if (up_true && click_on)
        {
            if (Input.GetMouseButtonDown(0))
                image.sprite = click;
            else if (Input.GetMouseButtonUp(0))
                image.sprite = normal;
        }
    }


    public void ups()
    {
        image.sprite = up;
        up_true = true;
    }

    public void downs()
    {
        image.sprite = normal;
        up_true = false;
    }
}

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

    void Update()
    {
        //if (Input.GetMouseButtonDown(0))
        //    image.sprite = click;
        //else if (Input.GetMouseButtonUp(0))
        //    image.sprite = normal;
    }


    public void ups()
    {
        image.sprite = up;
        Debug.Log("up");    
    }

    public void downs()
    {
        image.sprite = normal;
        Debug.Log("down");
    }
}

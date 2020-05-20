using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dialogue_choose : MonoBehaviour
{
    public Image image;

    public Sprite basic;
    public Sprite on;

    void OnEnable()
    {
        image.sprite = basic;
    }

    public void on_mouse()
    {
        image.sprite = on;
    }
    public void off_mouse()
    {
        image.sprite = basic;
    }
}

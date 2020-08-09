using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class normal_grow : MonoBehaviour
{
    float grow_num = 0;

    public bool red = false;

    void Awake()
    {
        gameObject.transform.localScale = new Vector3(grow_num, grow_num, grow_num);
    }

    void Update()
    {
        if (red)
        {
            if (grow_num < 1f && InputManager.instance.click_mod == 0)
            {
                grow_num += Time.deltaTime / 2f;
                gameObject.transform.localScale = new Vector3(grow_num * 8, grow_num * 8, grow_num * 48);
            }
        }
        else
        {
            if (grow_num <= 0.1f && InputManager.instance.click_mod == 0)
            {
                grow_num += Time.deltaTime / 20f;
                gameObject.transform.localScale = new Vector3(grow_num, grow_num, grow_num);
            }
        }
    }
}

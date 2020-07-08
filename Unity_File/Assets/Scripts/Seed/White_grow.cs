using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class White_grow : MonoBehaviour
{
    float grow_num = 0;
    float grow_num2 = 0;

    public GameObject up;
    public GameObject down;

    void Awake()
    {
        up.transform.localScale = new Vector3(grow_num, grow_num, grow_num);
        down.transform.localScale = new Vector3(grow_num2, grow_num2, grow_num2);
    }

    void Update()
    {
        if (InputManager.instance.click_mod == 0)
        {
            if (grow_num < 1f)
            {
                grow_num += Time.deltaTime / 2f;
                down.transform.localScale = new Vector3(grow_num, grow_num, grow_num);
            }
            else if (grow_num2 < 1f)
            {
                grow_num2 += Time.deltaTime / 5f;
                up.transform.localScale = new Vector3(grow_num2, grow_num2, grow_num2);
            }
        }
    }
}

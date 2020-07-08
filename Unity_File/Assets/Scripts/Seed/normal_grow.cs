using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class normal_grow : MonoBehaviour
{
    float grow_num = 0;

    void Awake()
    {
        gameObject.transform.localScale = new Vector3(grow_num, grow_num, grow_num);
    }

    void Update()
    {
        if (grow_num <= 0.1f && InputManager.instance.click_mod == 0)
        {
            grow_num += Time.deltaTime / 20f;
            gameObject.transform.localScale = new Vector3(grow_num, grow_num, grow_num);
        }
    }
}

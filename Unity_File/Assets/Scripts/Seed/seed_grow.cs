using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class seed_grow : MonoBehaviour
{
    float num = 0.5f;

    private void Awake()
    {
        gameObject.GetComponent<Renderer>().sharedMaterial.SetFloat("_Grow", num);
    }

    void Update()
    {
        if (InputManager.instance.click_mod == 0)
        {
            if (num > -0.5f)
            {
                num -= 0.25f * Time.deltaTime;
                gameObject.GetComponent<Renderer>().sharedMaterial.SetFloat("_Grow", num);
            }
            else
            {
                MeshRenderer mr = gameObject.GetComponent<MeshRenderer>();   // 일단 MeshRenderer 컴포넌트를 얻고
                mr.material.shader = Shader.Find("Custom/Grow2");                                 // 쉐이더를 찾아(이름으로) 변경
            }
        }
    }
}

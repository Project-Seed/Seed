using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager instance; // 현재 클레스를 인스턴트화

    public int click_mod = 0; // 0이면 기본, 1이면 메뉴

    public static InputManager Instance
    {
        get { return instance; }
    }

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Load_nodie : MonoBehaviour
{
    public static Load_nodie instance; // 현재 클레스를 인스턴트화

    public bool load_on = false;
    public int num;

    public static Load_nodie Instance
    {
        get { return instance; }
    }

    void Awake()
    {
        if (instance == null)         // 씬이 바뀌어도 유지
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
            Destroy(this.gameObject);

    }

    public void loads()
    {
        if (load_on)
        {
            GameSystem.instance.load_game(num);
            load_on = false;
        }
    }
}
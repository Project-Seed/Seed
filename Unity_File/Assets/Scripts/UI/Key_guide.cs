using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key_guide : MonoBehaviour
{
    public static Key_guide instance; // 현재 클레스를 인스턴트화

    public GameObject talk;
    public GameObject item;

    public static Key_guide Instance
    {
        get { return instance; }
    }
    void Start()
    {
        instance = this;
    }

    void Update()
    {
        
    }

    public void talk_on()
    {
        talk.SetActive(true);
    }
    public void talk_off()
    {
        talk.SetActive(false);
    }
}

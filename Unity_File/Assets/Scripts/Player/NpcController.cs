using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NpcController : MonoBehaviour
{
    private Dialogue dialogue; // 다이얼로그

    public GameObject name_position;
    public string npc_name;

    private void Start()
    {
        dialogue = GameObject.Find("Dialogue").GetComponent<Dialogue>();
    }


    private void OnTriggerEnter(Collider collision)
    {
        if (collision.name == "Player")
        {
            dialogue.open_on = true;
            dialogue.quest_on(gameObject, name_position, npc_name);
        }
    }
    private void OnTriggerExit(Collider collision)
    {
        if (collision.name == "Player")
        {
            dialogue.open_on = false;
            dialogue.quest_off();       
        }
    }
}

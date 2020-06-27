using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NpcController : MonoBehaviour
{
    public GameObject dialogue; // 다이얼로그

    public GameObject name_position;
    public string npc_name;


    private void OnTriggerEnter(Collider collision)
    {
        if (collision.name == "Player")
        {
            dialogue.GetComponent<Dialogue>().open_on = true;
            dialogue.GetComponent<Dialogue>().quest_on(gameObject, name_position, npc_name);
        }
    }
    private void OnTriggerExit(Collider collision)
    {
        if (collision.name == "Player")
        {
            dialogue.GetComponent<Dialogue>().open_on = false;
            dialogue.GetComponent<Dialogue>().quest_off();       
        }
    }
}

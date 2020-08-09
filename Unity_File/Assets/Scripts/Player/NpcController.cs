using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NpcController : MonoBehaviour
{
    private Dialogue dialogue; // 다이얼로그
    public Animator animator;

    public GameObject name_position;
    public string npc_name;

    private void Start()
    {
        dialogue = GameObject.Find("Dialogue").GetComponent<Dialogue>();
    }

    private void Update()
    {
        if (dialogue.dialogue_box.activeSelf == true)
            animator.SetBool("talk", true);
        else
            animator.SetBool("talk", false);
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

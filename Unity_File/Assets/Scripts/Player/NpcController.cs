using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NpcController : MonoBehaviour
{
    public GameObject dialogue; // 다이얼로그

    public GameObject name_position;

    void Start()
    {
        
    }

    void Update()
    {

    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.name == "Player")
        {
            dialogue.GetComponent<Dialogue>().quest_on(gameObject, name_position, "코난");
        }
    }
    private void OnTriggerExit(Collider collision)
    {
        if (collision.name == "Player")
        {
            dialogue.GetComponent<Dialogue>().quest_off();
        }
    }
}

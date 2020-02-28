using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NpcController : MonoBehaviour
{
    public GameObject dialogue; // 다이얼로그

    public Text text;
    public Text name;
    public GameObject texts;
    public GameObject names;
    public Camera cameras;

    public GameObject name_position;

    bool check = false;

    void Start()
    {
        
    }

    void Update()
    {
        if (check == true)
        {
            Vector3 screenPos = cameras.WorldToScreenPoint(transform.position);
            Vector3 screenPos2 = cameras.WorldToScreenPoint(name_position.transform.position);
            texts.transform.position = new Vector3(screenPos.x, screenPos.y, text.transform.position.z);
            names.transform.position = new Vector3(screenPos2.x, screenPos2.y, name.transform.position.z);
            name.text = "코난";

            if (Input.GetKeyDown(KeyCode.H))
            {
                if (dialogue.activeSelf == false)
                {
                    dialogue.SetActive(true);
                }
            }
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.name == "Player")
        {
            check = true;
            texts.SetActive(true);
            names.SetActive(true);
        }
    }
    private void OnTriggerExit(Collider collision)
    {
        if (collision.name == "Player")
        {
            check = false;
            texts.SetActive(false);
            names.SetActive(false);
        }
    }
}

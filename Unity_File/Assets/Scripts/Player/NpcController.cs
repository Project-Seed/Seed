using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NpcController : MonoBehaviour
{
    public GameObject dialogue; // 다이얼로그

    public Text text;
    public GameObject texts;
    public Camera cameras;

    bool check = false;

    void Start()
    {
        
    }

    void Update()
    {
        if (check == true)
        {
            Vector3 screenPos = cameras.WorldToScreenPoint(transform.position);
            float x = screenPos.x;
            text.transform.position = new Vector3(x, screenPos.y, text.transform.position.z);

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
        }
    }
    private void OnTriggerExit(Collider collision)
    {
        if (collision.name == "Player")
        {
            check = false;
            texts.SetActive(false);
        }
    }
}

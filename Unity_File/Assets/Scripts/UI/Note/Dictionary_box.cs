using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dictionary_box : MonoBehaviour
{
    public GameObject dictionarys;

    private void Start()
    {
        dictionarys = GameObject.Find("Dictionarys");
    }

    public void click()
    {
        dictionarys.GetComponent<Dictionarys>().Dictionary_click(gameObject);
    }
}

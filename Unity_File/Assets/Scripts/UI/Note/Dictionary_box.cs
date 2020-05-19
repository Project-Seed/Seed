using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dictionary_box : MonoBehaviour
{
    public GameObject dictionarys;
    public Image image;

    private void Start()
    {
        dictionarys = GameObject.Find("Dictionarys");
    }

    public void click()
    {
        dictionarys.GetComponent<Dictionarys>().Dictionary_click(gameObject);
    }
}

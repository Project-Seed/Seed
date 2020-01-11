using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dictionary_box : MonoBehaviour
{
    public GameObject dictionary;

    private void Start()
    {
        dictionary = GameObject.Find("Dictionary");
    }

    public void click()
    {
        dictionary.GetComponent<Dictionary>().Dictionary_click(gameObject);
    }
}

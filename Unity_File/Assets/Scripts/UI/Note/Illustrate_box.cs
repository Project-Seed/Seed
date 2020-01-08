using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Illustrate_box : MonoBehaviour
{
    public GameObject illustrate;

    private void Start()
    {
        illustrate = GameObject.Find("Illustrated");
    }

    public void click()
    {
        illustrate.GetComponent<Illustrate>().Illustrated_click(gameObject);
    }
}

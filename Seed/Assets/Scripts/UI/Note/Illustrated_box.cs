using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Illustrated_box : MonoBehaviour
{
    public GameObject illustrated;

    private void Start()
    {
        illustrated = GameObject.Find("Illustrated");
    }

    public void click()
    {
        illustrated.GetComponent<Illustrated>().Illustrated_click(gameObject);
    }
}

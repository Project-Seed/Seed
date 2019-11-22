using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory_box : MonoBehaviour
{
    public GameObject inventory;

    private void Start()
    {
        inventory = GameObject.Find("Inventory");
    }

    public void click()
    {
        inventory.GetComponent<Inventory>().Inventory_click(gameObject);
    }
}

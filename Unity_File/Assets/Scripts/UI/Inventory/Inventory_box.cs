using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Inventory_box : MonoBehaviour
{
    public GameObject inventory;
    public Image image;

    private void Start()
    {
        inventory = GameObject.Find("Inventory");
    }

    public void click()
    {
        inventory.GetComponent<Inventory>().Inventory_click(gameObject);
    }
}

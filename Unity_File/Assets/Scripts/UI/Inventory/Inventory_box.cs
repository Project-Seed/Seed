using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Inventory_box : MonoBehaviour
{
    public GameObject inventory;
    bool clicks = false;

    private void Start()
    {
        inventory = GameObject.Find("Inventory");
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            if (clicks == true)
            {
                Debug.Log("D");
            }
        }
    }

    public void OnMouseEnter()
    {
        clicks = true;   
    }
    public void OnMouseExit()
    {
        clicks = false; 
    }

    public void click()
    {
        inventory.GetComponent<Inventory>().Inventory_click(gameObject);
    }

    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCollier : MonoBehaviour
{
    public GameObject item_ui;

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.tag == "Item_text")
        {
            item_ui.SetActive(true);
            item_ui.GetComponent<Item_text>().text_reset(collision.name);
        }
    }
    private void OnTriggerExit(Collider collision)
    {
        if (collision.tag == "Item_text")
        {
            item_ui.SetActive(false);
        }
    }
}

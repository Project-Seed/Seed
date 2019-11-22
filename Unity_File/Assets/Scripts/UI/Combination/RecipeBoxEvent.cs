using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecipeBoxEvent : MonoBehaviour
{
    public GameObject itemCombination;

    private void Start()
    {
        itemCombination = GameObject.Find("ItemCombination");
    }

    public void click()
    {
        itemCombination.GetComponent<ItemCombination>().recipe_click(gameObject);
    }
}

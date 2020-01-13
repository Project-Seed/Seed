using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecipeBoxEvent : MonoBehaviour
{
    public GameObject Combination;

    private void Start()
    {
        Combination = GameObject.Find("Combination");
    }

    public void click()
    {
        Combination.GetComponent<Combination>().recipe_click(gameObject);
    }
}

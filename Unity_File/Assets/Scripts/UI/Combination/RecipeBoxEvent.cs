using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecipeBoxEvent : MonoBehaviour
{
    public GameObject Combination;
    public Image image;

    private void Start()
    {
        Combination = GameObject.Find("Combination");
    }

    public void click()
    {
        Combination.GetComponent<Combination>().recipe_click(gameObject);
    }
}

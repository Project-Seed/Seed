using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    public bool on = false;

    public void open()
    {
        if(on == false)
        {
            on = true;

            string recipe_choose = "portion";

            Eat_system.instance.eat_item(recipe_choose);

            StartCoroutine(GameObject.Find("BigItem_get").GetComponent<BigItem_get>().
                        active_on(recipe_choose, true));
        }
    }
}

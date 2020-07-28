using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    public bool on = false;
    public Animator animator;

    public void open()
    {
        if(on == false)
        {
            on = true;

            StartCoroutine(open_co());
        }
    }

    IEnumerator open_co()       
    {
        animator.SetTrigger("open");

        yield return new WaitForSeconds(0.5f);

        string recipe_choose = "portion";
        Eat_system.instance.eat_item(recipe_choose);

        StartCoroutine(GameObject.Find("BigItem_get").GetComponent<BigItem_get>().
                        active_on(recipe_choose, true));
    }
}

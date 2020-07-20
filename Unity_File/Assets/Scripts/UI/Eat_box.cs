using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Eat_box : MonoBehaviour
{
    public Image image;
    public Text text;

    private void Start()
    {
        StartCoroutine(die());
    }

    IEnumerator die() 
    {
        yield return new WaitForSeconds(2f);
        GameObject.Find("Eat_gui").GetComponent<Eat_system>().eat_remove();
        Destroy(gameObject);
    }
}

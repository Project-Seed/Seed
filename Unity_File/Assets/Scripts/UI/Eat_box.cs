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
        yield return new WaitForSeconds(3f);
        Destroy(gameObject);
    }
}

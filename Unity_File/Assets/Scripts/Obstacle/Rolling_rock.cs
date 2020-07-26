using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rolling_rock : MonoBehaviour
{
    public int cooltime = 0;
    private void Awake()
    {
        Destroy(gameObject, 30);
    }

    IEnumerator die()
    {
        cooltime = 1;
        GameObject.Find("State").GetComponent<State>().hp_down(4);
        yield return new WaitForSeconds(3f);

        Destroy(gameObject);

        for (int i=0; i<50; i++)
        {

            yield return new WaitForSeconds(0.02f);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Player" && cooltime == 0)
        {
            StartCoroutine(die());
        }
    }
}

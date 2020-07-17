using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gress : MonoBehaviour
{
    public List<GameObject> gress;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(gress[0].transform.eulerAngles.x);

        if (other.gameObject.name == "Player")
        {
            for (int i = 0; i < gress.Count; i++)
            {
                gress[i].transform.rotation = Quaternion.Euler(new Vector3(-30, gress[i].transform.eulerAngles.y, 0));
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            for (int i = 0; i < gress.Count; i++)
            {
                gress[i].transform.rotation = Quaternion.Euler(new Vector3(0, gress[i].transform.eulerAngles.y, 0));
            }
        }
    }
}

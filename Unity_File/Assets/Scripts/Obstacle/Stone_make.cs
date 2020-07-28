using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stone_make : MonoBehaviour
{
    public GameObject rolling_rock;
    public GameObject make_point;

    public int cooltime = 0;


    IEnumerator making()
    {
        cooltime = 1;

        Instantiate(rolling_rock, make_point.transform.position, make_point.transform.rotation, make_point.transform);
        yield return new WaitForSeconds(0.5f);
        Instantiate(rolling_rock, new Vector3(make_point.transform.position.x, make_point.transform.position.y, make_point.transform.position.z - 1), make_point.transform.rotation, make_point.transform);
        yield return new WaitForSeconds(0.5f);
        Instantiate(rolling_rock, new Vector3(make_point.transform.position.x, make_point.transform.position.y, make_point.transform.position.z + 1), make_point.transform.rotation, make_point.transform);
        yield return new WaitForSeconds(0.5f);

        yield return new WaitForSeconds(15f);
        cooltime = 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Player" && cooltime == 0)
        { 
            StartCoroutine(making());
        }
    }
}

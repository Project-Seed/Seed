using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class place_Name : MonoBehaviour
{
    public string names;
    public string info;
    public string position;

    public GameObject info_ob;
    public Text names_t;
    public Text info_t;
    public Text position_t;

    public void on()
    {
        info_ob.SetActive(true);
        info_ob.transform.position = new Vector3(gameObject.transform.position.x + 175, gameObject.transform.position.y - 125, 0);

        names_t.text = names;
        info_t.text = info;
        position_t.text = position;
    }

    public void off()
    {
        info_ob.SetActive(false);
    }
}

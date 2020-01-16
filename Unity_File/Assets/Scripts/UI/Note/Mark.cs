using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mark : MonoBehaviour
{
    GameObject map;

    private void Awake()
    {
        map = GameObject.Find("Map");
    }

    public void on_mark() // 마커 위에 커서가 있을경우
    {
        map.GetComponent<Map>().on_mark(gameObject);
    }
    public void off_mark() // 마커 위에 커서가 없을경우
    {
        map.GetComponent<Map>().off_mark(gameObject);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMap : MonoBehaviour
{
    public GameObject lookarea;
    public GameObject cameras;

    public GameObject character_point;
    public GameObject character;

    void Start()
    {
        
    }

    void Update()
    {
        lookarea.transform.rotation = Quaternion.Euler(0,0,-cameras.transform.rotation.eulerAngles.y + 180 - 45);
        character_point.transform.rotation = Quaternion.Euler(0, 0, -character.transform.rotation.eulerAngles.y);
    }
}

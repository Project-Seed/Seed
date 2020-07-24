using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMap : MonoBehaviour
{
    public GameObject lookarea;
    public GameObject cameras;

    public GameObject character_point;
    public GameObject character;

    public GameObject point;

    public GameObject player;
    public GameObject map;

    void Start()
    {
        player = GameObject.Find("Player");
    }

    void Update()
    {
        lookarea.transform.rotation = Quaternion.Euler(0,0,-cameras.transform.rotation.eulerAngles.y - 45 + 135);
        character_point.transform.rotation = Quaternion.Euler(0, 0, -character.transform.rotation.eulerAngles.y + 90);


        switch (PlayerState.instance.radiation_level)
        {
            case 0:
                point.transform.rotation = Quaternion.Euler(0, 0, 90);
                break;

            case 1:
                point.transform.rotation = Quaternion.Euler(0, 0, 45);
                break;

            case 2:
                point.transform.rotation = Quaternion.Euler(0, 0, 0);
                break;

            case 3:
                point.transform.rotation = Quaternion.Euler(0, 0, -45);
                break;

            case 4:
                point.transform.rotation = Quaternion.Euler(0, 0, -90);
                break;
        }

        map.transform.localPosition = new Vector2(player.transform.position.x + 500, player.transform.position.z - 1500);
    }
}

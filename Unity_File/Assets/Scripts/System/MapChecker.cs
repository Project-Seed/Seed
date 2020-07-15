using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapChecker : MonoBehaviour
{
    private float distance;
    // Start is called before the first frame update
    void Start()
    {
        distance = 5f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Physics.Raycast(transform.position, -transform.up, out RaycastHit hit, distance))
        {
            Debug.Log("Map Name : " + hit.collider.gameObject.name);
            GameSystem.instance.map_name = hit.collider.gameObject.name;
        }
    }
}

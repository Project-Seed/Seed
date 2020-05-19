using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExampleItem : MonoBehaviour, IItem
{
    public MeshRenderer render;

    public void eat()
    {

    }

    public void Collided()
    {
        Debug.Log("Collied with Example Item!");
    }
}

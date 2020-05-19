using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExampleItem : MonoBehaviour, IItem
{
    public MeshRenderer render;
    public SphereCollider collider;

    private void Awake()
    {
        render = GetComponent<MeshRenderer>();
        collider = GetComponent<SphereCollider>();
    }

    public void eat()
    {
        render.enabled = false;
        collider.enabled = false;

        StartCoroutine(making());
    }

    public void make()
    {
        render.enabled = true;
        collider.enabled = true;
    }

    IEnumerator making()                  // 이단 점프를 막기 위해 점프시 1초간 점프금지
    {
        yield return new WaitForSeconds(30f);
        make();
    }

    public void Collided()
    {
        Debug.Log("Collied with Example Item!");
    }
}

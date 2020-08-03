using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExampleItem : MonoBehaviour
{
    public List<GameObject> items;
    public bool mountain;

    private MeshRenderer render;
    private new SphereCollider collider;
    private ParticleSystem twinkle;

    private void Awake()
    {
        render = GetComponent<MeshRenderer>();
        collider = GetComponent<SphereCollider>();
        twinkle = GetComponentInChildren<ParticleSystem>();
    }

    public void eat()
    {
        render.enabled = false;
        collider.enabled = false;
        if (twinkle)
            twinkle.Stop();
        StartCoroutine(making());
    }

    public void make()
    {
        render.enabled = true;
        collider.enabled = true;
        if (twinkle)
            twinkle.Play();
    }

    IEnumerator making()
    {
        yield return new WaitForSeconds(30f);
        make();
    }
}

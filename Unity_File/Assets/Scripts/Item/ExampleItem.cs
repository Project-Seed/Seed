using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExampleItem : MonoBehaviour
{
    public List<GameObject> items;
    public List<int> items_true = new List<int>(); // 조건에 맞는 아이템

    GameObject gameObjects;

    private MeshRenderer render;
    private new SphereCollider collider;
    private ParticleSystem twinkle;

    private void Awake()
    {
        collider = GetComponent<SphereCollider>();
        twinkle = GetComponentInChildren<ParticleSystem>();

        make();
    }

    public void eat()
    {
        collider.enabled = false;
        if (twinkle)
            twinkle.Stop();

        Destroy(gameObjects);

        StartCoroutine(making());
    }

    public void make()
    {
        collider.enabled = true;
        if (twinkle)
            twinkle.Play();

        int num = Random.Range(0, items_true.Count);

        gameObjects = Instantiate(items[items_true[num]], gameObject.transform.position, Quaternion.identity, gameObject.transform);
        gameObjects.transform.localScale = new Vector3(1, 1, 1);
        gameObject.name = items[items_true[num]].name;
    }

    IEnumerator making()
    {
        yield return new WaitForSeconds(30f);
        make();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class State : MonoBehaviour
{
    public GameObject heart;
    public List<GameObject> hearts;

    public GameObject viewport;

    void Start()
    {
        for(int i=0; i< PlayerState.instance.max_hp / 2; i++)
        {
            hearts.Add(Instantiate(heart, new Vector3(0, 0, 0), Quaternion.identity, viewport.transform));
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            hit(1);
        }
        if (Input.GetKeyDown(KeyCode.Y))
        {
            hit(5);
        }
    }

    public void hit(int num)
    {
        for(int i=0; i<num; i++)
        {
            int hp_int = (PlayerState.instance.hp - 1) / 2;
            hearts[hp_int].GetComponent<Heart>().hp_reduce((PlayerState.instance.hp - 1) % 2);

            PlayerState.instance.hp--;
        }
    }
}

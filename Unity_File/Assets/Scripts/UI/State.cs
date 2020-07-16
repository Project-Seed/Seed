using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class State : MonoBehaviour
{
    public GameObject heart;
    public List<GameObject> hearts;

    public GameObject radi;
    public List<GameObject> radis;

    public GameObject viewport_hp;
    public GameObject viewport_radi;

    void Start()
    {
        for(int i=0; i< PlayerState.instance.max_hp / 2; i++)
        {
            hearts.Add(Instantiate(heart, new Vector3(0, 0, 0), Quaternion.identity, viewport_hp.transform));
        }
        for (int j = 0; j < PlayerState.instance.max_radiation / 2; j++)
        {
            radis.Add(Instantiate(radi, new Vector3(0, 0, 0), Quaternion.identity, viewport_radi.transform));
        }

        StartCoroutine(Update_Radiation());
        StartCoroutine(hit_radi());
    }

    IEnumerator Update_Radiation()
    {
        while (true)
        {
            if (PlayerState.instance.max_radiation != PlayerState.instance.radiation)
            {
                switch (PlayerState.instance.radiation_level)
                {
                    case 1:
                        addiction(1);
                        break;

                    case 2:
                        addiction(2);
                        break;

                    case 3:
                        addiction(3);
                        break;
                }
            }

            yield return new WaitForSeconds(10f);
        }
    }

    IEnumerator hit_radi()
    {
        while (true)
        {
            if (PlayerState.instance.max_radiation == PlayerState.instance.radiation)
                hit(1);

            yield return new WaitForSeconds(3f);
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

    public void addiction(int num)
    {
        for (int i = 0; i < num; i++)
        {
            int radi_int = PlayerState.instance.radiation / 2;
            radis[radi_int].GetComponent<Radi>().radi_up(PlayerState.instance.radiation % 2);

            PlayerState.instance.radiation++;
        }
    }
}

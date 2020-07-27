using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class State : MonoBehaviour
{
    public static State instance; // 현재 클레스를 인스턴트화

    public GameObject heart;
    public List<GameObject> hearts;

    public GameObject radi;
    public List<GameObject> radis;

    public GameObject viewport_hp;
    public GameObject viewport_radi;

    public bool white_seed; // 하얀 충돌했는지


    public static State Instance
    {
        get { return instance; }
    }

    private void Awake()
    {
        instance = this;
    }

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

    private void Update()
    {
        // 임시, 반드시 삭제요망
        if (Input.GetKeyDown(KeyCode.T))
        {
            hp_down(3);
        }
    }

    IEnumerator Update_Radiation()
    {
        while (true)
        {
            if (PlayerState.instance.max_radiation != PlayerState.instance.radiation && InputManager.instance.click_mod == 0 && white_seed == false)
            {
                switch (PlayerState.instance.radiation_level)
                {
                    case 0:
                        yield return new WaitForSeconds(1f);
                        break;

                    case 1:
                        radi_up(1);
                        yield return new WaitForSeconds(10f);
                        break;

                    case 2:
                        radi_up(2);
                        yield return new WaitForSeconds(10f);
                        break;

                    case 3:
                        radi_up(3);
                        yield return new WaitForSeconds(10f);
                        break;

                    case 4:
                        radi_up(5);
                        yield return new WaitForSeconds(1f);
                        break;
                }
            }
            else if(PlayerState.instance.radiation > 0 && InputManager.instance.click_mod == 0 && white_seed == true)
            {
                radi_down(1);
                yield return new WaitForSeconds(2f);
            }
            else 
                yield return new WaitForSeconds(10f);
        }
    }

    IEnumerator hit_radi()
    {
        while (true)
        {
            if (PlayerState.instance.max_radiation == PlayerState.instance.radiation && InputManager.instance.click_mod == 0)
                hp_down(1);

            yield return new WaitForSeconds(3f);
        }
    }

    public void hp_up(int num)
    {
        for (int i = 0; i < num; i++)
        {
            int hp_int = PlayerState.instance.hp / 2;
            hearts[hp_int].GetComponent<Heart>().hp_up(PlayerState.instance.hp % 2);

            PlayerState.instance.hp++;
        }
    }

    public void hp_down(int num)
    {
        for(int i=0; i<num; i++)
        {
            if (PlayerState.instance.hp != 0)
            {
                int hp_int = (PlayerState.instance.hp - 1) / 2;
                hearts[hp_int].GetComponent<Heart>().hp_down((PlayerState.instance.hp - 1) % 2);

                PlayerState.instance.hp--;
            }
        }
    }

    public void radi_up(int num)
    {
        for (int i = 0; i < num; i++)
        {
            int radi_int = PlayerState.instance.radiation / 2;
            radis[radi_int].GetComponent<Radi>().radi_up(PlayerState.instance.radiation % 2);

            PlayerState.instance.radiation++;
        }
    }

    public void radi_down(int num)
    {
        for (int i = 0; i < num; i++)
        {
            int radi_int = (PlayerState.instance.radiation - 1) / 2;
            radis[radi_int].GetComponent<Radi>().radi_down((PlayerState.instance.radiation - 1) % 2);

            PlayerState.instance.radiation--;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Key_guide : MonoBehaviour
{
    public static Key_guide instance; // 현재 클레스를 인스턴트화

    public GameObject talk;
    public GameObject item;
    public GameObject climb;
    public GameObject objects;
    public GameObject door;

    public GameObject item_name_ob;
    public Text item_name;

    public GameObject object_name_ob;
    public Text object_name;

    public GameObject white;

    public static Key_guide Instance
    {
        get { return instance; }
    }
    void Start()
    {
        instance = this;
    }

    public void item_name_on(string name, Vector3 ts)
    {
        item_name_ob.SetActive(true);
        item_name_ob.transform.position = new Vector3(ts.x, ts.y + 70, ts.z);
        item_name.text = name;
    }
    public void item_name_off()
    {
        item_name_ob.SetActive(false);
    }

    public void talk_on()
    {
        talk.SetActive(true);
    }
    public IEnumerator talk_ing()
    {
        white.SetActive(true);
        white.transform.position = talk.transform.position;
        for (int i = 0; i < 20; i++)
        {
            if (i < 5)
            {
                white.GetComponent<Image>().color = new Color(1, 1, 1, i / 5f);
                yield return new WaitForSeconds(0.01f);
            }
            else if (i == 5)
            {
                talk.SetActive(false);
                yield return new WaitForSeconds(0.01f);
            }
            else
            {
                white.GetComponent<Image>().color = new Color(1, 1, 1, (20 - i) / 15f);
                yield return new WaitForSeconds(0.01f);
            }
        }
        white.SetActive(false);
    }
    public void talk_off()
    {
        talk.SetActive(false);
    }

    public void item_on()
    {
        item.SetActive(true);
    }
    public IEnumerator item_ing()
    {
        white.SetActive(true);
        white.transform.position = item.transform.position;
        for (int i=0; i<20; i++)
        {
            if (i < 5)
            {
                white.GetComponent<Image>().color = new Color(1,1,1, i / 5f);
                yield return new WaitForSeconds(0.01f);
            }
            else  if(i == 5)
            {
                item.SetActive(false);
                yield return new WaitForSeconds(0.01f);
            }
            else
            {
                white.GetComponent<Image>().color = new Color(1, 1, 1, (20 - i) / 15f);
                yield return new WaitForSeconds(0.01f);
            }
        }
        white.SetActive(false);
    }
    public void item_off()
    {
        item.SetActive(false);
    }

    public void climb_on()
    {
        climb.SetActive(true);
    }
    public IEnumerator climb_ing()
    {
        white.SetActive(true);
        white.transform.position = climb.transform.position;
        for (int i = 0; i < 20; i++)
        {
            if (i < 5)
            {
                white.GetComponent<Image>().color = new Color(1, 1, 1, i / 5f);
                yield return new WaitForSeconds(0.01f);
            }
            else if (i == 5)
            {
                climb.SetActive(false);
                yield return new WaitForSeconds(0.01f);
            }
            else
            {
                white.GetComponent<Image>().color = new Color(1, 1, 1, (20 - i) / 15f);
                yield return new WaitForSeconds(0.01f);
            }
        }
        white.SetActive(false);
    }
    public void climb_off()
    {
        climb.SetActive(false);
    }

    public void door_on()
    {
        door.SetActive(true);
    }
    public IEnumerator door_ing()
    {
        white.SetActive(true);
        white.transform.position = door.transform.position;
        for (int i = 0; i < 20; i++)
        {
            if (i < 5)
            {
                white.GetComponent<Image>().color = new Color(1, 1, 1, i / 5f);
                yield return new WaitForSeconds(0.01f);
            }
            else if (i == 5)
            {
                door.SetActive(false);
                yield return new WaitForSeconds(0.01f);
            }
            else
            {
                white.GetComponent<Image>().color = new Color(1, 1, 1, (20 - i) / 15f);
                yield return new WaitForSeconds(0.01f);
            }
        }
        white.SetActive(false);
    }
    public void door_off()
    {
        door.SetActive(false);
    }

    public void object_on(string name, Vector3 ts)
    {
        object_name_ob.SetActive(true);
        object_name_ob.transform.position = new Vector3(ts.x, ts.y + 70, ts.z);
        object_name.text = name;

        objects.SetActive(true);
    }
    public IEnumerator object_ing()
    {
        white.SetActive(true);
        white.transform.position = objects.transform.position;
        for (int i = 0; i < 20; i++)
        {
            if (i < 5)
            {
                white.GetComponent<Image>().color = new Color(1, 1, 1, i / 5f);
                yield return new WaitForSeconds(0.01f);
            }
            else if (i == 5)
            {
                object_name_ob.SetActive(false);
                objects.SetActive(false);
                yield return new WaitForSeconds(0.01f);
            }
            else
            {
                white.GetComponent<Image>().color = new Color(1, 1, 1, (20 - i) / 15f);
                yield return new WaitForSeconds(0.01f);
            }
        }
        white.SetActive(false);
    }
    public void object_off()
    {
        object_name_ob.SetActive(false);
        objects.SetActive(false);
    }
}

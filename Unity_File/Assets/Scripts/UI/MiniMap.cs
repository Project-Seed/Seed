using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniMap : MonoBehaviour
{
    public GameObject lookarea;
    public GameObject cameras;

    public GameObject character_point;
    public GameObject character;

    public GameObject point;

    public GameObject player;
    public GameObject map;

    public List<GameObject> marks;
    public GameObject mark_pre;
    public List<GameObject> mini_marks;
    public List<GameObject> mini_marks_far;
    public GameObject zeros;


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

        for (int i = 0; i < mini_marks.Count; i++)
        {
            if ((mini_marks[i].transform.position - character_point.transform.position).magnitude > 115)
            {
                Vector2 a = new Vector3(1500, 1500, 0);

                mini_marks_far[i].SetActive(true);
                mini_marks[i].SetActive(false);
                mini_marks_far[i].transform.localPosition = (mini_marks[i].transform.position - character_point.transform.position).normalized * 115;
            }
            else
            {
                mini_marks_far[i].SetActive(false);
                mini_marks[i].SetActive(true);
            }
        }
    }

    public void mark_re()
    {
        for(int i=0; i<mini_marks.Count;i++)
        {
            Destroy(mini_marks[i].gameObject);
            Destroy(mini_marks_far[i].gameObject);
        }
        mini_marks.Clear();
        mini_marks_far.Clear();

        for(int i=0; i<marks.Count; i++)
        {
            GameObject mark_ = Instantiate(mark_pre, new Vector3(marks[i].GetComponent<RectTransform>().anchoredPosition.x, marks[i].GetComponent<RectTransform>().anchoredPosition.y, 0),
                Quaternion.identity, map.transform); // ItemSpawner 밑 자식으로 복제
            mark_.transform.localPosition = new Vector2(marks[i].GetComponent<RectTransform>().anchoredPosition.x, marks[i].GetComponent<RectTransform>().anchoredPosition.y);
            mark_.GetComponent<Image>().color = marks[i].GetComponent<Image>().color;
            mini_marks.Add(mark_);

            GameObject mark_2 = Instantiate(mark_pre, new Vector3(marks[i].GetComponent<RectTransform>().anchoredPosition.x, marks[i].GetComponent<RectTransform>().anchoredPosition.y, 0),
                Quaternion.identity, zeros.transform); // ItemSpawner 밑 자식으로 복제
            mark_2.transform.localPosition = new Vector2(marks[i].GetComponent<RectTransform>().anchoredPosition.x, marks[i].GetComponent<RectTransform>().anchoredPosition.y);
            mark_2.GetComponent<Image>().color = marks[i].GetComponent<Image>().color;
            mini_marks_far.Add(mark_2);
        }
    }
}

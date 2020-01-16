using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Map : MonoBehaviour
{
    public Text TimePosition;

    string time_switch = null;
    int time_hour;
    int time_minute;

    public GameObject map_bg;
    Vector3 move_start; // 이동 할때 누르기 시작시 좌표
    Vector3 move_now; // 이동 할때 누르기 시작할때 이미지 좌표

    bool push_bg = false;
    bool push_mark = false;
    public GameObject mark_maker_ob;
    public GameObject mark;
    GameObject marks_ob; // 눌러진 마크
    public Text mark_make_text;
    public Text mark_dis_text;
    int mark_click_mod = 0; // 지도클릭 0, 마크클릭 1

    void Update()
    {
        // 시간 위치 표시
        Vector3 pos = Input.mousePosition;

        if(GameSystem.instance.time < 60 * 12)
        {
            time_switch = "AM";
            time_hour = (int)GameSystem.instance.time / 60;
            time_minute = (int)GameSystem.instance.time % 60;
        }
        else
        {
            time_switch = "PM";
            time_hour = ((int)GameSystem.instance.time - 60 * 12) / 60;
            time_minute = ((int)GameSystem.instance.time - 60 * 12) % 60;
        }

        TimePosition.text = "시간 " + time_switch + " " + time_hour.ToString() + ":" + time_minute.ToString() + "\n위치 (" + ((int)pos.x).ToString() + ", " + ((int)pos.y).ToString() + ")";


        // 지도 이동
        if (Input.GetMouseButtonDown(0))
        {
            move_start = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
            move_now = map_bg.transform.position;
        }
        else if(Input.GetMouseButton(0))
        {
            map_bg.transform.position = move_now - (move_start - new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
        }


        // 지도 확대 축소
        float wheelInput = Input.GetAxis("Mouse ScrollWheel");
        if (wheelInput > 0)
        {
            map_bg.transform.localScale = new Vector3(map_bg.transform.localScale.x + 0.1f, map_bg.transform.localScale.y + 0.1f, 1);
        }
        else if (wheelInput < 0)
        {
            map_bg.transform.localScale = new Vector3(map_bg.transform.localScale.x - 0.1f, map_bg.transform.localScale.y - 0.1f, 1);
        }



        // 마커 생성
        if (Input.GetMouseButtonDown(1) && push_bg == true && push_mark == false && mark_maker_ob.activeSelf == false)
        {
            mark_maker_ob.SetActive(true);
            mark_maker_ob.transform.position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
            mark_make_text.color = Color.white;
            mark_dis_text.color = Color.grey;
            mark_click_mod = 0;
        }
        else if (Input.GetMouseButtonDown(1) && push_bg == true && push_mark == true && mark_maker_ob.activeSelf == false)
        {
            mark_maker_ob.SetActive(true);
            mark_maker_ob.transform.position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
            mark_make_text.color = Color.grey;
            mark_dis_text.color = Color.white;
            mark_click_mod = 1;
        }
    }

    // 마커 생성
    public void on_bg() // 지도 위에 커서가 있을경우
    {
        push_bg = true;
    }
    public void off_bg() // 지도 위에 커서가 없을경우
    {
        push_bg = false;
    }
    public void on_mark(GameObject mark_ob) // 마커 위에 커서가 있을경우
    {
        push_mark = true;
        marks_ob = mark_ob;
    }
    public void off_mark(GameObject mark_ob) // 마커 위에 커서가 없을경우
    {
        push_mark = false;
    }

    public void mark_make()
    {
        if (mark_click_mod == 0)
        {
            GameObject marks = Instantiate(mark, new Vector3(mark_maker_ob.transform.position.x, mark_maker_ob.transform.position.y, 0), Quaternion.identity, map_bg.transform); // ItemSpawner 밑 자식으로 복제
            mark_maker_ob.SetActive(false);
        }
    }
    public void mark_dis()
    {
        if (mark_click_mod == 1)
        {
            mark_maker_ob.SetActive(false);
            Destroy(marks_ob);
        }
    }
} 
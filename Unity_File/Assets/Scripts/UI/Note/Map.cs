﻿using System.Collections;
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

    int map_scale = 10; // 확대 축소 비율 제한 3~13

    bool push_bg = false;
    bool push_mark = false;
    public GameObject mark_maker_ob;
    public GameObject mark;
    GameObject marks_ob; // 눌러진 마크
    public Text mark_make_text;
    public Text mark_dis_text;
    int mark_click_mod = 0; // 지도클릭 0, 마크클릭 1

    public GameObject player;
    public GameObject model;
    public GameObject player_pick;

    public MiniMap miniMap;

    public GameObject warp;
    public float warp_point_x;
    public float warp_point_y;
    public float warp_point_z;

    private void Awake()
    {
        Camera.main.orthographicSize = 5;

        player = GameObject.Find("Player");
        miniMap = GameObject.Find("MiniMap").GetComponent<MiniMap>();
    }

    void Update()
    {
        // 지도 이동
        if (Input.GetMouseButtonDown(0))
        {
            move_start = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
            move_now = map_bg.transform.position;
        }
        else if(Input.GetMouseButton(0))
        {
            Vector3 move_position = move_now - (move_start - new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));

            if(move_position.x >= 200 && move_position.x <= 1700 && move_position.y >= -690 && move_position.y <= 1550)
                map_bg.transform.position = move_position;
            else
            {
                move_start = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
                move_now = map_bg.transform.position;
            }
        }

        
        // 지도 확대 축소
        //float wheelInput = Input.GetAxis("Mouse ScrollWheel");
        //if (wheelInput > 0)
        //{
        //    if (map_scale <= 12)
        //    {
        //        map_scale++;
        //        map_bg.transform.localScale = new Vector3(map_bg.transform.localScale.x + 0.1f, map_bg.transform.localScale.y + 0.1f, 1);


        //        /*
        //        Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize- wheelInput, 3, 5);
        //        wheelInput *= Vector3.Distance(map_bg.transform.position, map_bg.transform.position + (Camera.main.ScreenToViewportPoint(Input.mousePosition) - map_bg.transform.position)) * 0.0005f;
        //        map_bg.transform.position += (Camera.main.ScreenToViewportPoint(Input.mousePosition) - map_bg.transform.position) * wheelInput;
        //        */
        //        //map_bg.transform.Translate((960 - Input.mousePosition.x) * map_scale / 20, (540 - Input.mousePosition.y) * map_scale / 20, 0);
        //    }
        //}
        //else if (wheelInput < 0)
        //{
        //    if (map_scale >= 4)
        //    {
        //        map_scale--;
        //        map_bg.transform.localScale = new Vector3(map_bg.transform.localScale.x - 0.1f, map_bg.transform.localScale.y - 0.1f, 1);


        //        /*
        //        Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize - wheelInput, 3, 5);
        //        wheelInput *= Vector3.Distance(map_bg.transform.position, map_bg.transform.position + (Camera.main.ScreenToViewportPoint(Input.mousePosition) - map_bg.transform.position)) * 0.0005f;
        //        map_bg.transform.position += (Camera.main.ScreenToViewportPoint(Input.mousePosition) - map_bg.transform.position) * wheelInput;
        //        */
        //        //map_bg.transform.Translate((960 - Input.mousePosition.x) * map_scale / 20, (540 - Input.mousePosition.y) * map_scale / 20, 0);
        //    }
        //}



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

        player_pick.transform.localPosition = new Vector2(-player.transform.position.x - 500, -player.transform.position.z + 1500);
        player_pick.transform.rotation = Quaternion.Euler(0,0,-model.transform.rotation.eulerAngles.y + 90);
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
            GameObject mark_ = Instantiate(mark, new Vector3(mark_maker_ob.transform.position.x, mark_maker_ob.transform.position.y, 0), 
                Quaternion.identity, map_bg.transform); // ItemSpawner 밑 자식으로 복제
            mark_.GetComponent<Image>().color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f), 1);
            miniMap.marks.Add(mark_);
            miniMap.mark_re();

            mark_maker_ob.SetActive(false);
        }
    }
    public void mark_dis()
    {
        if (mark_click_mod == 1)
        {
            mark_maker_ob.SetActive(false);

            miniMap.marks.Remove(marks_ob);
            Destroy(marks_ob);
            miniMap.mark_re();
        }
    }

    public void warp_on(int warp_point)
    {
        warp.SetActive(true);

        switch(warp_point)
        {
            case 1:
                warp_point_x = 361;
                warp_point_y = 75.5f;
                warp_point_z = 434;
                break;
        }
    }
    public void warp_ok()
    {
        warp.SetActive(false);
        GameObject.Find("Note").SetActive(false);
        player.transform.position = new Vector3(warp_point_x,warp_point_y,warp_point_z);
    }

    public void warp_no()
    {
        warp.SetActive(false);
    }
} 
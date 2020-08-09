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

    int map_scale = 10; // 확대 축소 비율 제한 3~13

    bool push_bg = false;
    bool push_mark = false;
    public GameObject mark_maker_ob;
    public GameObject mark;
    GameObject marks_ob; // 눌러진 마크
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

            if(move_position.x >= 200 - 750 && move_position.x <= 1700 + 750 && move_position.y >= -690 - 750 && move_position.y <= 1550 + 750)
                map_bg.transform.position = move_position;
            else
            {
                move_start = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
                move_now = map_bg.transform.position;
            }
        }

        // 마커 생성
        if (Input.GetMouseButtonDown(1) && push_bg == true && push_mark == false && mark_maker_ob.activeSelf == false)
        {
            mark_maker_ob.SetActive(true);
            mark_maker_ob.transform.position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
            mark_click_mod = 0;
        }
        else if (Input.GetMouseButtonDown(1) && push_bg == true && push_mark == true && mark_maker_ob.activeSelf == false)
        {
            mark_maker_ob.SetActive(true);
            mark_maker_ob.transform.position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
            mark_click_mod = 1;
        }

        player_pick.transform.localPosition = new Vector2(-player.transform.position.x - 500, -player.transform.position.z + 1500);
        player_pick.transform.rotation = Quaternion.Euler(0,0,-model.transform.rotation.eulerAngles.y + 90);
    }

    private void OnEnable()
    {
        player_pick.transform.localPosition = new Vector2(-player.transform.position.x - 500, -player.transform.position.z + 1500);
        map_bg.GetComponent<RectTransform>().localPosition = -player_pick.GetComponent<RectTransform>().localPosition;
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

        switch (warp_point)
        {
            case 1: // 마스
                warp_point_x = 361;
                warp_point_y = 75.5f;
                warp_point_z = 434;
                break;

            case 2: // 홉스
                warp_point_x = 503;
                warp_point_y = 59;
                warp_point_z = 808;
                break;

            case 3: // 터널
                warp_point_x = 73;
                warp_point_y = 41;
                warp_point_z = 1482;
                break;

            case 4: // 유격훈련장
                warp_point_x = -1642;
                warp_point_y = 100;
                warp_point_z = 1169;
                break;

            case 5: // 전파탑
                warp_point_x = -1835;
                warp_point_y = 100;
                warp_point_z = 1375;
                break;

            case 6: // 공학자의 마을
                warp_point_x = 584;
                warp_point_y = 68;
                warp_point_z = 2900;
                break;
        }
    }
    public void warp_ok()
    {
        warp.SetActive(false);
        GameObject.Find("Note").SetActive(false);
        player.transform.position = new Vector3(warp_point_x,warp_point_y,warp_point_z);

        GameSystem.instance.sound_start(9);
    }

    public void warp_no()
    {
        warp.SetActive(false);
    }
} 
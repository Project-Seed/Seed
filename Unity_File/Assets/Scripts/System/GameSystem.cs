using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSystem : MonoBehaviour
{
    public static GameSystem instance; // 현재 클레스를 인스턴트화

    public float radiation; // 현재 피폭 수치
    public float max_radiation; // 전체 피폭 수치
    public int radiation_level; // 현재 피폭 레벨

    public float hp; // 현재 HP
    public float max_hp; // 전체 HP

    public List<Dictionary<string, string>> item_list; // 아이템 DB
    public List<Dictionary<string, string>> combination_list; // 조합 테이블
    public List<Dictionary<string, string>> quest_list; // 퀘스트 DB

    public List<string> item_time; // 먹은 아이템 순서
    public Dictionary<string, int> item_num = new Dictionary<string, int>(); // 먹은 아이템 갯수

    public Dictionary<int, int> quest_state = new Dictionary<int, int>(); // 퀘스트 상태  0 시작전, 1 진행중, 2 클리어 대기, 3 완료

    public float time = 660; // 현재 시간


    [Flags]
    public enum Mode
    {
        BasicMode = 0,    // 기본
        ThrowMode = 1,    // 발사 모드
        CinemaMode = 2    // 시네마틱 모드
    }
    Mode mode;

    public int GetModeNum()
    {
        return (int)mode;
    }

    public void SetMode(int state)
    {
        switch (state)
        {
            case 0:
                mode = Mode.BasicMode;
                break;
            case 1:
                mode = Mode.ThrowMode;
                break;
            case 2:
                mode = Mode.CinemaMode;
                break;
        }
    }

    public static GameSystem Instance
    {
        get { return instance;}
    }

    private void Awake()
    {
        instance = this;
        /*
        if(instance == null)         // 씬이 바뀌어도 유지
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
            Destroy(this.gameObject);
        */

        item_list = CSV_Reader.Read("Item_Table"); // 아이탬 로드
        for(int i=0; i<item_list.Count; i++)
        {
            item_num.Add(item_list[i]["name"], 0); // 먹은 아이템 갯수에 아이템 이름 등록
        }

        combination_list = CSV_Reader.Read("Combination_Table"); // 조합 로드

        quest_list = CSV_Reader.Read("Quest_Table"); // 퀘스트 로드
        for (int i = 0; i < quest_list.Count; i++)
        {
            quest_state.Add(i+1, 0); // 퀘스트 진행상태 등록
        }
    }

    public void Gamestart()
    {
        SceneManager.LoadScene("Game_Scene");
    }

    public string item_search(string name, string category) // 아이템 서치 알고리즘
    {
        for (int i = 0; i < item_list.Count; i++)
        {
            if (item_list[i]["name"] == name)
            {
                return item_list[i][category];
            }
        }

        return null;
    }
}

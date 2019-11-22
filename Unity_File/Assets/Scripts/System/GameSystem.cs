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

    public List<Dictionary<string, string>> item_list; // 먹는 아이템
    public List<Dictionary<string, string>> combination_list; // 조합 테이블

    public List<string> item_time; // 먹은 아이템 순서
    public Dictionary<string, int> item_num = new Dictionary<string, int>();

    public static GameSystem Instance
    {
        get
        {
            return instance;
        }
    }

    private void Awake()
    {
        if(instance == null)         // 씬이 바뀌어도 유지
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
            Destroy(this.gameObject);


        item_list = CSV_Reader.Read("Item_Table"); // 아이탬 로드
        for(int i=0; i<item_list.Count; i++)
        {
            item_num.Add(item_list[i]["name"], 0);
        }
        combination_list = CSV_Reader.Read("Combination_Table");
    }

    public void Gamestart()
    {
        SceneManager.LoadScene("Game_Scene");
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

using System.Linq;
using System.Xml.Serialization;
using System.Runtime.Serialization;
using System.Xml.Linq;
using System.Xml;


public class GameSystem : MonoBehaviour
{
    public static GameSystem instance; // 현재 클레스를 인스턴트화

    public List<Dictionary<string, string>> item_list; // 아이템 DB
    public List<Dictionary<string, string>> combination_list; // 조합 테이블
    public List<Dictionary<string, string>> quest_list; // 퀘스트 DB
    public List<Dictionary<string, string>> world_list; // 지도 방사능 DB

    public List<string> item_time; // 먹은 아이템 순서
    public Dictionary<string, int> item_num = new Dictionary<string, int>(); // 먹은 아이템 갯수

    public Dictionary<int, int> quest_state = new Dictionary<int, int>(); // 퀘스트 상태  0 시작전, 1 진행중, 2 클리어 대기, 3 완료

    public static bool switch_mode;

    public Quest_quick quest_quick;
    public Dictionarys dictionarys;
    public PlayerState playerstate;

    public GameObject character;
    public GameObject dialogue;
    public GameObject dialogue_box;

    public bool talk_trigger = false; // 주문하신 대화하는중인지 체크
    public GameObject talk_npc_ob; // 주문하신 대화하는 npc오브젝트

    public string map_name;
    public string map_name_ko;

    public Dictionarys dictionary_sc;

    public string save_path = "";

    public void load_game(int num)
    {
        XDocument save_data = XDocument.Load(save_path + "./save_data" + num.ToString() + ".xml");

        Light_system.instance.time = float.Parse(save_data.Element("root").Element("solo").Element("time").Value);
        character.transform.position = new Vector3(
            float.Parse(save_data.Element("root").Element("solo").Element("ch_po_x").Value),
            float.Parse(save_data.Element("root").Element("solo").Element("ch_po_y").Value),
            float.Parse(save_data.Element("root").Element("solo").Element("ch_po_z").Value));
        character.transform.rotation = new Quaternion(
            float.Parse(save_data.Element("root").Element("solo").Element("ch_ro_x").Value),
            float.Parse(save_data.Element("root").Element("solo").Element("ch_ro_y").Value),
            float.Parse(save_data.Element("root").Element("solo").Element("ch_ro_z").Value),0);
        playerstate.hp = int.Parse(save_data.Element("root").Element("solo").Element("hp").Value);
        playerstate.radiation = int.Parse(save_data.Element("root").Element("solo").Element("radiation").Value);

        item_num = new Dictionary<string, int>();
        foreach (var load in save_data.Element("root").Element("item_num").Elements())
            item_num.Add(load.Name.LocalName, Convert.ToInt32(load.Value)); // dictionary 정석

        quest_state = new Dictionary<int, int>();
        foreach (var load in save_data.Element("root").Element("quest_state").Elements())
            quest_state.Add(Convert.ToInt32(load.Name.LocalName.Substring(4)), Convert.ToInt32(load.Value)); // key가 int

        Quest_clear_system.instance.clear_trigger = new Dictionary<int, int>();
        foreach (var load in save_data.Element("root").Element("clear_trigger").Elements())
            Quest_clear_system.instance.clear_trigger.Add(Convert.ToInt32(load.Name.LocalName.Substring(4)), Convert.ToInt32(load.Value));

        dictionarys.dictionary_num = new Dictionary<string, bool>();
        foreach (var load in save_data.Element("root").Element("dictionary_num").Elements())
            dictionarys.dictionary_num.Add(load.Name.LocalName, bool.Parse(load.Value));

        item_time = new List<string>();
        foreach (var load in save_data.Element("root").Element("item_time").Elements())
            item_time.Add(load.Name.LocalName); // list 정석


        quest_quick.quest_re();
    }

    public void save_gema(int num)
    {
        XElement save_data = new XElement("root",
            new XElement("solo", 
                new XElement("time", Light_system.instance.time), 
                new XElement("ch_po_x", character.transform.position.x),
                new XElement("ch_po_y", character.transform.position.y),
                new XElement("ch_po_z", character.transform.position.z),
                new XElement("ch_ro_x", character.transform.rotation.x),
                new XElement("ch_ro_y", character.transform.rotation.y),
                new XElement("ch_ro_z", character.transform.rotation.z),
                new XElement("hp", playerstate.hp),
                new XElement("radiation", playerstate.radiation),
                new XElement("map_name_ko", map_name_ko),
                new XElement("save_time", DateTime.Now.ToString(("yyyy-MM-dd HH:mm:ss tt")))),
            new XElement("item_num", item_num.Select(kv => new XElement(kv.Key, kv.Value))), // dictionary 정석
            new XElement("quest_state", quest_state.Select(kv => new XElement("char" + kv.Key.ToString(), kv.Value))), // key가 int
            new XElement("clear_trigger", Quest_clear_system.instance.clear_trigger.Select(kv => new XElement("char" + kv.Key.ToString(), kv.Value))),
            new XElement("dictionary_num", dictionarys.dictionary_num.Select(kv => new XElement(kv.Key, kv.Value))),
            new XElement("item_time", item_time.Select(kv => new XElement(kv))) // list 정석
            ); 

        XDocument document = new XDocument();
        document.Add(save_data);

        document.Save(save_path + "./save_data" + num.ToString() + ".xml");
    }

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
        Mode currentMode = mode;
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
        switch_mode = currentMode != mode ? true : false;
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

        world_list = CSV_Reader.Read("World_Table"); // 지역 방사능 로드

        dictionary_sc.before_awake();
    }

    private void Start()
    {
        Load_nodie.instance.loads();
    }

    public void GameStart()
    {
        LoadingSceneManager.LoadScene("Tutorial_Scene");
    }

    public IEnumerator TutorialEnd()
    {
        GameObject.Find("Kira").GetComponent<MeshRenderer>().enabled = true;
        yield return new WaitForSeconds(2f);

        Quest_clear_system.instance.clear_trigger[11]++;
        LoadingSceneManager.LoadScene("Game_SceneNew");
    }

    public string item_search(string name, string category) // 아이템 서치 알고리즘
    {
        for (int i = 0; i < item_list.Count; i++)
        {
            if (item_list[i]["name"].Equals(name))
            {
                return item_list[i][category];
            }
        }

        return null;
    }
}

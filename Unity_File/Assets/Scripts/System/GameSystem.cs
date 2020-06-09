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

    public List<string> item_time; // 먹은 아이템 순서
    public Dictionary<string, int> item_num = new Dictionary<string, int>(); // 먹은 아이템 갯수

    public Dictionary<int, int> quest_state = new Dictionary<int, int>(); // 퀘스트 상태  0 시작전, 1 진행중, 2 클리어 대기, 3 완료

    public float time = 660; // 현재 시간

    public static bool switch_mode;


    public GameObject dialogue;
    public GameObject dialogue_box;


    [Serializable]
    public class GameData
    {
        public List<string> D_item_time;
        public Dictionary<string, int> D_item_num = new Dictionary<string, int>();
        public Dictionary<int, int> D_quest_state = new Dictionary<int, int>();
        public float D_time;
    }

    public void load_game()
    {
        /*
        XElement rootElement = XElement.Load("./Assets/Character.xml");
        item_num = new Dictionary<string, int>();
        foreach (var el in rootElement.Elements())
        {
            item_num.Add(el.Name.LocalName, Convert.ToInt32(el.Value));
        }
        */


        XDocument rootElement = XDocument.Load("./Assets/Character2.xml");

        XElement aa = rootElement.Element("root");
        XElement a = aa.Element("a1");
        Debug.Log(aa.ToString());
        Debug.Log(a.ToString());
        item_num = new Dictionary<string, int>();
        foreach (var el in a.Elements())
        {
            item_num.Add(el.Name.LocalName, Convert.ToInt32(el.Value));
        }
    }

    public void save_gema()
    {
        XElement e = new XElement("root", 
            new XElement("a1", item_num.Select(kv => new XElement(kv.Key, kv.Value))));

        /*
        XElement e1 = new XElement("root", item_num.Select(kv => new XElement(kv.Key, kv.Value)));
        XElement e2 = new XElement("root2", item_num.Select(kv => new XElement(kv.Key, kv.Value)));
        */

        //el.Save("./Assets/Character.xml");

        /*
        XmlDocument document = new XmlDocument();

        //document.Load(el.CreateReader());
        XmlElement a = ToXmlElement(el);
        document.AppendChild(a);
        */

        XDocument document = new XDocument();
        document.Add(e);

        document.Save("./Assets/Character2.xml");
    }

    /*
    public XmlElement ToXmlElement(XElement el)
    {
        var doc = new XmlDocument();
        doc.Load(el.CreateReader());
        return doc.DocumentElement;
    }
    */


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
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.J))
        {
            save_gema();
        }

        if (Input.GetKey(KeyCode.K))
        {
            load_game();
        }

        if (Input.GetKey(KeyCode.Z))
            talk_start(1);
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

    public void talk_start(int num) // 혼잣말 등 대화 시작
    {
        if (InputManager.instance.click_mod == 0 && dialogue_box.activeSelf == false)
        {
            dialogue_box.SetActive(true);
            InputManager.instance.click_mod = 1;
            dialogue.GetComponent<Text_system>().StartDialogue(num);
        }
    }
}

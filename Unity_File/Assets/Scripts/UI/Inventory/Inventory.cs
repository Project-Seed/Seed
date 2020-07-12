using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public GameObject viewport;
    public GameObject content;
    public GameObject quick_button;
    public bool quick_bool = false;
    public Image quick_bg;
    public Sprite quick_image_off;
    public Sprite quick_image_on;
    public List<GameObject> item_box;
    public GameObject spand_button;

    public Text item_names;
    public Text item_explanation;
    public Image item_image;
    
    private string item_choose = null; // 어떤 아이템을 눌렀는지
    // private int item_move = 0; // 몇번째 아이템에 커서가 있는지

    public bool quick_mod = false;
    public string quick_name;

    public Sprite alpha;

    public Inven_quick in_qu;

    
    private void Awake()
    {
        for(int i=0; i<40; i++)
        {
            GameObject gameObject = Instantiate(content, new Vector3(0, 0, 0), Quaternion.identity, viewport.transform); // viewport 밑 자식으로 복제
            gameObject.name = "Inventory_box_" + i;
            item_box.Add(gameObject);

            if (i < GameSystem.instance.item_time.Count)
            {
                item_box[i].GetComponent<Inventory_box>().image.sprite = Resources.Load<Sprite>("Item2D/" + GameSystem.instance.item_time[i]);
                item_box[i].GetComponentInChildren<Text>().text = "x " + GameSystem.instance.item_num[GameSystem.instance.item_time[i]].ToString();
            }
            else
            {
                item_box[i].GetComponent<Inventory_box>().image.sprite = alpha;
                item_box[i].GetComponentInChildren<Text>().text = "";
            }
        }

        item_explanation.text = "";
        item_names.text = "";
        item_image.sprite = alpha;
    }

    private void OnEnable()
    {
        InputManager.instance.game_stop();
    }
    private void OnDisable()
    {
        InputManager.instance.game_start();
    }

    private void Update()
    {
        for (int i = 0; i < 40; i++)
        {
            if (i < GameSystem.instance.item_time.Count)
            {
                item_box[i].GetComponent<Inventory_box>().image.sprite = Resources.Load<Sprite>("Item2D/" + GameSystem.instance.item_time[i]);
                item_box[i].GetComponentInChildren<Text>().text = "x " + GameSystem.instance.item_num[GameSystem.instance.item_time[i]].ToString();
            }
            else
            {
                item_box[i].GetComponent<Inventory_box>().image.sprite = alpha;
                item_box[i].GetComponentInChildren<Text>().text = "";
            }
        }

        if (Input.GetKeyDown(KeyCode.Q))
        { 
            if(quick_bool == true)
            {
                if(quick_mod == false)
                {
                    quick_mod = true;
                    quick_name = item_choose;
                    quick_bg.sprite = quick_image_on;
                }
                else
                {
                    quick_mod = false;
                    quick_bg.sprite = quick_image_off;
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha1) && quick_mod == true)
        {
            in_qu.item_set(0, quick_name);
            quick_mod = false;
            quick_bg.sprite = quick_image_off;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2) && quick_mod == true)
        {
            in_qu.item_set(1, quick_name);
            quick_mod = false;
            quick_bg.sprite = quick_image_off;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3) && quick_mod == true)
        {
            in_qu.item_set(2, quick_name);
            quick_mod = false;
            quick_bg.sprite = quick_image_off;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4) && quick_mod == true)
        {
            in_qu.item_set(3, quick_name);
            quick_mod = false;
            quick_bg.sprite = quick_image_off;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5) && quick_mod == true)
        {
            in_qu.item_set(4, quick_name);
            quick_mod = false;
            quick_bg.sprite = quick_image_off;
        }
    }

    public void Inventory_click(GameObject gameObject) // 인벤토리 내용물 클릭
    {
        string item_name = gameObject.name;
        item_name = item_name.Substring(14, item_name.Length - 14);
        int item_int = System.Convert.ToInt32(item_name);

        try 
        { item_choose = GameSystem.instance.item_time[item_int]; }
        catch 
        {}

        if (item_int < GameSystem.instance.item_time.Count)
        {
            item_explanation.text = GameSystem.instance.item_search(item_choose, "explanation_ko");
            item_names.text = GameSystem.instance.item_search(item_choose, "name_ko");
            item_image.sprite = gameObject.GetComponent<Inventory_box>().image.sprite;

            if (GameSystem.instance.item_search(item_choose, "category") == "seed")
            {
                quick_button.SetActive(true);
                quick_bool = true;

                spand_button.SetActive(false);
            }
            else if (GameSystem.instance.item_search(item_choose, "category") == "consumable")
            {
                quick_button.SetActive(true);
                quick_bool = true;

                spand_button.SetActive(true);
            }
            else
            {
                quick_button.SetActive(false);
                quick_bool = false;

                spand_button.SetActive(false);
            }
        }
    }

    public void spand()
    {
        // 붙여온거에다 gameObject.SetActive(false); 이거만 추가

        if (GameSystem.instance.item_num[item_choose] >= 1)
        {
            switch (GameSystem.instance.item_search(item_choose, "name"))
            {
                case "portion":
                    gameObject.SetActive(false);

                    if (PlayerState.instance.hp + 10 < PlayerState.instance.max_hp)
                        PlayerState.instance.hp += 10;
                    else
                        PlayerState.instance.hp = PlayerState.instance.max_hp;

                    GameSystem.instance.item_num[item_choose]--;

                    if (GameSystem.instance.item_num[item_choose] == 0)
                        GameSystem.instance.item_time.Remove(item_choose); 
                    break;

                case "mini_latter":
                    gameObject.SetActive(false);

                    InputManager.instance.click_mod = 1;
                    Quest_clear_system.instance.clear_trigger[8]++;
                    Instantiate(Resources.Load<GameObject>("Tutorial/Mini_latter"), GameObject.Find("Canvas").transform);
                    break;
            }
        }
    }
}

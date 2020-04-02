using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public GameObject viewport;
    public GameObject content;
    public GameObject quick_button;
    public List<GameObject> item_box;

    public Text item_names;
    public Text item_explanation;
    
    private string item_choose = null; // 어떤 아이템을 눌렀는지
    // private int item_move = 0; // 몇번째 아이템에 커서가 있는지

    public bool quick_mod = false;
    public string quick_name;
    public GameObject quick_image;
    
    private void Awake()
    {
        for(int i=0; i<50; i++)
        {
            GameObject gameObject = Instantiate(content, new Vector3(0, 0, 0), Quaternion.identity, viewport.transform); // viewport 밑 자식으로 복제
            gameObject.name = "Inventory_box_" + i;
            item_box.Add(gameObject);

            if (i < GameSystem.instance.item_time.Count)
            {
                item_box[i].GetComponent<Image>().sprite = Resources.Load<Sprite>("Item2D/" + GameSystem.instance.item_time[i]);
                item_box[i].GetComponentInChildren<Text>().text = GameSystem.instance.item_num[GameSystem.instance.item_time[i]].ToString();
            }
            else
            {
                item_box[i].GetComponent<Image>().sprite = null;
                item_box[i].GetComponentInChildren<Text>().text = "";
            }
        }
    }

    private void OnEnable()
    {
        InputManager.instance.click_mod = 1;
    }
    private void OnDisable()
    {
        InputManager.instance.click_mod = 0;
        quick_off();
    }

    private void Update()
    {
        for (int i = 0; i < 50; i++)
        {
            if (i < GameSystem.instance.item_time.Count)
            {
                item_box[i].GetComponent<Image>().sprite = Resources.Load<Sprite>("Item2D/" + GameSystem.instance.item_time[i]);
                item_box[i].GetComponentInChildren<Text>().text = GameSystem.instance.item_num[GameSystem.instance.item_time[i]].ToString();
            }
            else
            {
                item_box[i].GetComponent<Image>().sprite = null;
                item_box[i].GetComponentInChildren<Text>().text = "";
            }
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

            if (GameSystem.instance.item_search(item_choose, "category") == "consumable")
                quick_button.SetActive(true);
            else
                quick_button.SetActive(false);
        }
    }

    public void quick_click() // 단축키 등록 클릭
    {
        quick_mod = true;
        quick_image.SetActive(true);
        quick_name = item_choose;
    }

    public void quick_off()
    {
        quick_mod = false;
        quick_image.SetActive(false);
    }
}

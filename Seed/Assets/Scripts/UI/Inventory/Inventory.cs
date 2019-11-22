using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public GameObject viewport;
    public GameObject content;
    public List<GameObject> item_box;

    public Text item_names;
    public Text item_explanation;
    
    private string item_choose = null; // 어떤 아이템을 눌렀는지
    private int item_move = 0; // 몇번째 아이템에 커서가 있는지
    
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
        if (item_int < GameSystem.instance.item_time.Count)
        {
            item_choose = GameSystem.instance.item_time[item_int];

            for (int i = 0; i < GameSystem.instance.item_list.Count; i++)
            {
                if (GameSystem.instance.item_list[i]["name"] == item_choose)
                {
                    item_explanation.text = GameSystem.instance.item_list[i]["explanation_ko"];
                    item_names.text = GameSystem.instance.item_list[i]["name_ko"];
                    break;
                }
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Illustrate : MonoBehaviour
{
    public GameObject viewport;
    public GameObject content;
    public List<GameObject> item_box;

    public Text item_names;
    public Text item_explanation;
    public Image item_image;

    private string item_choose = null; // 어떤 아이템을 눌렀는지

    // Start is called before the first frame update
    void Awake()
    {
        for (int i = 0; i < GameSystem.instance.dictionary_num.Count; i++)
        {
            GameObject gameObject = Instantiate(content, new Vector3(0, 0, 0), Quaternion.identity, viewport.transform); // viewport 밑 자식으로 복제
            gameObject.name = "Illustrate_box_" + i;
            item_box.Add(gameObject);
            item_box[i].GetComponent<Image>().sprite = Resources.Load<Sprite>("Item2D/" + GameSystem.instance.dictionary_time[i]);

            if (GameSystem.instance.dictionary_num[GameSystem.instance.dictionary_time[i]] == true)
            {
                item_box[i].GetComponent<Image>().color = new Color(1, 1, 1, 1);
            }
            else
            {
                item_box[i].GetComponent<Image>().color = new Color(1, 1, 1, 0.2f);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < GameSystem.instance.dictionary_num.Count; i++)
        {
            if (GameSystem.instance.dictionary_num[GameSystem.instance.dictionary_time[i]] == true)
            {
                item_box[i].GetComponent<Image>().color = new Color(1, 1, 1, 1);
            }
            else
            {
                item_box[i].GetComponent<Image>().color = new Color(1, 1, 1, 0.2f);
            }
        }
    }

    public void Illustrated_click(GameObject gameObject) // 도감 내용물 클릭
    {
        string item_name = gameObject.name;
        item_name = item_name.Substring(15, item_name.Length - 15);
        int item_int = System.Convert.ToInt32(item_name);
        item_choose = GameSystem.instance.dictionary_time[item_int];

        if (GameSystem.instance.dictionary_num[item_choose] == true)
        {
            for (int i = 0; i < GameSystem.instance.item_list.Count; i++)
            {
                if (GameSystem.instance.item_list[i]["name"] == item_choose)
                {
                    item_explanation.text = GameSystem.instance.item_list[i]["explanation_ko"];
                    item_names.text = GameSystem.instance.item_list[i]["name_ko"];
                    item_image.sprite = Resources.Load<Sprite>("Item2D/" + item_choose);
                    break;
                }
            }
        }
    }
}

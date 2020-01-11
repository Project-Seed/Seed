using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dictionary : MonoBehaviour
{
    public GameObject viewport;
    public GameObject content;
    public List<GameObject> item_box;

    public Text item_names;
    public Text item_explanation;
    public Image item_image;

    public List<string> category;
    int now_category = 0; // 현재 카테고리

    private string item_choose = null; // 어떤 아이템을 눌렀는지

    bool have_on = false; // false는 일반정렬 true는 있는것만 표기

    public InputField search_data;

    void Awake()
    {
        for (int i = 0; i < GameSystem.instance.dictionary_num.Count; i++)
        {
            GameObject gameObject = Instantiate(content, new Vector3(0, 0, 0), Quaternion.identity, viewport.transform); // viewport 밑 자식으로 복제
            gameObject.name = "Illustrate_box_" + i;
            item_box.Add(gameObject);
            item_box[i].GetComponent<Image>().sprite = Resources.Load<Sprite>("Item2D/" + GameSystem.instance.dictionary_time[i]);

            if (GameSystem.instance.dictionary_num[GameSystem.instance.dictionary_time[i]] == true)
                item_box[i].GetComponent<Image>().color = new Color(1, 1, 1, 1);
            else
                item_box[i].GetComponent<Image>().color = new Color(1, 1, 1, 0.2f);
        }

        reset_dictionary();
    }

    void Update()
    {
        for (int i = 0; i < item_box.Count; i++)
        {
            if (GameSystem.instance.dictionary_num[GameSystem.instance.dictionary_time[i]] == true)
                item_box[i].GetComponent<Image>().color = new Color(1, 1, 1, 1);
            else
                item_box[i].GetComponent<Image>().color = new Color(1, 1, 1, 0.2f);
        }
    }

    public void Dictionary_click(GameObject gameObject) // 도감 내용물 클릭
    {
        string item_name = gameObject.name;
        item_name = item_name.Substring(15, item_name.Length - 15);
        int item_int = System.Convert.ToInt32(item_name);
        item_choose = GameSystem.instance.dictionary_time[item_int];

        if (GameSystem.instance.dictionary_num[item_choose] == true)
        {
            item_explanation.text = GameSystem.instance.item_search(item_choose, "explanation_ko");
            item_names.text = GameSystem.instance.item_search(item_choose, "name_ko");
            item_image.sprite = Resources.Load<Sprite>("Item2D/" + item_choose);
        }
    }

    public void have_click() // 보유중만 보기 클릭
    {
        if (have_on == false)
            have_on = true;
        else
            have_on = false;

        reset_dictionary();
    }

    public void category_click(int num)
    {
        now_category = num;

        reset_dictionary();
    }

    public void search_click()
    {
        reset_dictionary();
        search_data.text = "";
    }

    public void reset_dictionary()
    {
        for (int i = 0; i < item_box.Count; i++)
        {
            if ((GameSystem.instance.dictionary_num[GameSystem.instance.dictionary_time[i]] == false && have_on == true) ||
                (GameSystem.instance.item_search(GameSystem.instance.dictionary_time[i], "category") != category[now_category]) ||
                ((search_data.text.Length != 0) && !(GameSystem.instance.item_search(GameSystem.instance.dictionary_time[i], "name_ko").Contains(search_data.text))))
                item_box[i].SetActive(false);
            else
                item_box[i].SetActive(true);
        }
    }
}

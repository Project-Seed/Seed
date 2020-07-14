using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dictionarys : MonoBehaviour
{
    public static Dictionarys instance; // 현재 클레스를 인스턴트화

    public List<string> dictionary_time; // 도감 순서
    public Dictionary<string, bool> dictionary_num = new Dictionary<string, bool>(); // 도감 false면 미획득 true면 획득


    public GameObject viewport;
    public GameObject content;
    public List<GameObject> item_box;

    public Text item_names;
    public Text item_explanation;
    public Image item_image;

    public List<string> category;
    int now_category = 0; // 현재 카테고리
    public List<GameObject> category_on_ob;

    private string item_choose = null; // 어떤 아이템을 눌렀는지

    bool have_on = false; // false는 일반정렬 true는 있는것만 표기

    public InputField search_data;

    public static Dictionarys Instance
    {
        get { return instance; }
    }

    private void OnEnable()
    {
        if (item_choose != null)
            StartCoroutine(TypeSentence(GameSystem.instance.item_search(item_choose, "explanation_ko")));
    }


    void Awake()
    {
        instance = this;

        for (int i = 0; i < GameSystem.instance.item_list.Count; i++)
        {
            // 도감에 아이템 이름 등록
            dictionary_time.Add(GameSystem.instance.item_list[i]["name"]);
            dictionary_num.Add(GameSystem.instance.item_list[i]["name"], false);
        }



        for (int i = 0; i < dictionary_num.Count; i++)
        {
            GameObject gameObject = Instantiate(content, new Vector3(0, 0, 0), Quaternion.identity, viewport.transform); // viewport 밑 자식으로 복제
            gameObject.name = "Illustrate_box_" + i;
            item_box.Add(gameObject);
            item_box[i].GetComponent<Dictionary_box>().image.sprite = Resources.Load<Sprite>("Item2D/" + dictionary_time[i]);

            if (dictionary_num[dictionary_time[i]] == true)
                item_box[i].GetComponent<Dictionary_box>().image.color = new Color(1, 1, 1, 1);
            else
                item_box[i].GetComponent<Dictionary_box>().image.color = new Color(1, 1, 1, 0.1f);
        }

        reset_dictionary();
    }

    void Update()
    {
        for (int i = 0; i < item_box.Count; i++)
        {
            if (dictionary_num[dictionary_time[i]] == true)
                item_box[i].GetComponent<Dictionary_box>().image.color = new Color(1, 1, 1, 1);
            else
                item_box[i].GetComponent<Dictionary_box>().image.color = new Color(1, 1, 1, 0.1f);
        }
    }

    public void Dictionary_click(GameObject gameObject) // 도감 내용물 클릭
    {
        string item_name = gameObject.name;
        item_name = item_name.Substring(15, item_name.Length - 15);
        int item_int = System.Convert.ToInt32(item_name);
        item_choose = dictionary_time[item_int];

        if (dictionary_num[item_choose] == true)
        {
            if (item_choose != null)
                StartCoroutine(TypeSentence(GameSystem.instance.item_search(item_choose, "explanation_ko")));

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

        for(int i=0; i<category_on_ob.Count; i++)
        {
            category_on_ob[i].SetActive(false);
        }
        category_on_ob[num].SetActive(true);

        reset_dictionary();
    }

    public void search_click()
    {
        reset_dictionary();
    }

    public void reset_dictionary()
    {
        for (int i = 0; i < item_box.Count; i++)
        {
            if ((dictionary_num[dictionary_time[i]] == false && have_on == true) ||
                (GameSystem.instance.item_search(dictionary_time[i], "category") != category[now_category]) ||
                ((search_data.text.Length != 0) && !(GameSystem.instance.item_search(dictionary_time[i], "name_ko").Contains(search_data.text))))
                item_box[i].SetActive(false);
            else
                item_box[i].SetActive(true);
        }
    }

    IEnumerator TypeSentence(string sentence) // 한글자씩 출력
    {
        if (sentence != "")
        {
            item_explanation.text = "";
            foreach (char letter in sentence.ToCharArray())
            {
                if (letter == '$')
                    item_explanation.text += "\n";
                else if (letter == '@')
                    item_explanation.text += ",";
                else
                    item_explanation.text += letter;
                yield return null;
            }
        }
    }
}

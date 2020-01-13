using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CombinationItem : MonoBehaviour
{
    // 조합할때 레시피를 고른후 필요한 아이템을 보여주는 코드

    public GameObject viewport;
    public GameObject content;
    public List<GameObject> item_box;

    public Text text;
    public Image image;

    int use_num; // 조합에 필요한 갯수
    int have_num;  // 그중 가지고 있는 갯수

    int seach_num_get;

    public void Awake()
    {
        gameObject.name = gameObject.name.Substring(0, gameObject.name.Length - 7);

        GameObject itemCombination = GameObject.Find("Combination");
        seach_num_get = itemCombination.GetComponent<Combination>().seach_num;

        image.sprite = Resources.Load<Sprite>("Item2D/" + gameObject.name);
        for (int i = 1; i <= 4; i++)
        {
            if (GameSystem.instance.combination_list[seach_num_get]["name" + i] == gameObject.name)
            {
                use_num = System.Convert.ToInt32(GameSystem.instance.combination_list[seach_num_get]["num" + i]);

                for (int j = 0; j < use_num; j++)
                {
                    GameObject gameObject1 = Instantiate(content, new Vector3(0, 0, 0), Quaternion.identity, viewport.transform);
                    gameObject1.GetComponent<Image>().sprite = Resources.Load<Sprite>("Item2D/" + gameObject.name);
                    item_box.Add(gameObject1);
                }
                break;
            }
        }


        if (GameSystem.instance.item_num.ContainsKey(gameObject.name))
            have_num = GameSystem.instance.item_num[gameObject.name];
        else
            have_num = 0;

        text.text = have_num.ToString() + "/" + use_num.ToString();

        for (int j = 0; j < use_num; j++)
        {
            if (j < have_num)
                item_box[j].GetComponent<Image>().color = Color.white;
            else
                item_box[j].GetComponent<Image>().color = Color.grey;
        }
    }

    public void Update()
    {
        if (GameSystem.instance.item_num.ContainsKey(gameObject.name))
            have_num = GameSystem.instance.item_num[gameObject.name];
        else
            have_num = 0;

        text.text = have_num.ToString() + "/" + use_num.ToString();

        for (int j = 0; j < use_num; j++)
        {
            if(j < have_num)
                item_box[j].GetComponent<Image>().color = Color.white;
            else
                item_box[j].GetComponent<Image>().color = Color.grey;
        }
    }
}

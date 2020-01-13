using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Combination : MonoBehaviour
{
    public GameObject viewport;
    public GameObject content;

    public GameObject item_vieport;
    public GameObject item_content;

    public List<GameObject> recipe_box;
    public List<GameObject> item_box;

    public Image image;                // 결과 아이템 이미지의 원래 이미지

    private string recipe_choose = null; // 어떤 레시피를 눌렀는지
    public int seach_num = 0; // 어떤 레시피(조합 테이블) 몇번째 것인가?

    private void Awake()
    {
        for (int i = 0; i < GameSystem.instance.combination_list.Count; i++)
        {
            GameObject gameObject1 = Instantiate(content, new Vector3(0, 0, 0), Quaternion.identity, viewport.transform); // viewport 밑 자식으로 복제
            gameObject1.name = GameSystem.instance.combination_list[i]["name"];
            recipe_box.Add(gameObject1);
        }
    }

    private void Update()
    {
        for (int i = 0; i < GameSystem.instance.combination_list.Count; i++)
        {
            recipe_box[i].GetComponent<Image>().sprite = Resources.Load<Sprite>("Item2D/" + GameSystem.instance.combination_list[i]["name"]);
        }
    }

    public void recipe_click(GameObject gameObject)
    {
        for (int i = 0; i < item_box.Count; i++) // 있던 객체 삭제
        {
           Destroy(item_box[i]);
        }
        item_box.Clear();

        recipe_choose = gameObject.name;

        Image new_image = gameObject.GetComponent<Image>();    
        image.sprite = new_image.sprite;

        for (int i = 0; i < GameSystem.instance.combination_list.Count; i++)
        {
            if(GameSystem.instance.combination_list[i]["name"] == recipe_choose)
            {
                seach_num = i;
                break;
            }
        }
        for (int i = 0; i < System.Convert.ToInt32(GameSystem.instance.combination_list[seach_num]["item_num"]); i++)
        {
            item_content.name = GameSystem.instance.combination_list[seach_num]["name" + (i + 1)];
            GameObject gameObject2 = Instantiate(item_content, new Vector3(0, 0, 0), Quaternion.identity, item_vieport.transform);
            item_box.Add(gameObject2);
        }
    }

    public void combination()
    {
        bool ok = true;

        for (int i = 1; i <= System.Convert.ToInt32(GameSystem.instance.combination_list[seach_num]["item_num"]); i++)
        {
            if (GameSystem.instance.item_num[GameSystem.instance.combination_list[seach_num]["name" + i]] < System.Convert.ToInt32(GameSystem.instance.combination_list[seach_num]["num" + i]))
                ok = false;
        }

        if (ok)
        {
            if (GameSystem.instance.item_num[recipe_choose] == 0) // 못먹었던 아이템이면
                GameSystem.instance.item_time.Add(recipe_choose);
            GameSystem.instance.item_num[recipe_choose] += 1;

            if (Dictionary.instance.dictionary_num[recipe_choose] == false) // '한번도' 못먹었던 아이템이면 (도감용)
                Dictionary.instance.dictionary_num[recipe_choose] = true;

            for (int i = 1; i <= System.Convert.ToInt32(GameSystem.instance.combination_list[seach_num]["item_num"]); i++)
            {
                GameSystem.instance.item_num[GameSystem.instance.combination_list[seach_num]["name" + i]] -= System.Convert.ToInt32(GameSystem.instance.combination_list[seach_num]["num" + i]);
                if (GameSystem.instance.item_num[GameSystem.instance.combination_list[seach_num]["name" + i]] == 0)
                    GameSystem.instance.item_time.Remove(GameSystem.instance.combination_list[seach_num]["name" + i]);
            }
        }
    }
}
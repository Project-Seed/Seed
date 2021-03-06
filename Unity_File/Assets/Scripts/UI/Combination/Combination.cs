﻿using System.Collections;
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

    int type = 0; // 0 Select , 1 Combin
    public GameObject select;
    public GameObject combin;

    public Text name;
    public Text explane;

    public GameObject succses;
    public GameObject fail;

    private void Awake()
    {
        for (int i = 0; i < GameSystem.instance.combination_list.Count; i++)
        {
            GameObject gameObject1 = Instantiate(content, new Vector3(0, 0, 0), Quaternion.identity, viewport.transform); // viewport 밑 자식으로 복제
            gameObject1.name = GameSystem.instance.combination_list[i]["name"];
            gameObject1.GetComponent<RecipeBoxEvent>().image.sprite = Resources.Load<Sprite>("Item2D/" + GameSystem.instance.combination_list[i]["name"]);
            recipe_box.Add(gameObject1);
        }

        recipe_click(recipe_box[0]);
    }

    private void OnEnable()
    {
        InputManager.instance.game_stop();
        succses.SetActive(false);
        fail.SetActive(false);
    }
    private void OnDisable()
    {
        InputManager.instance.game_start();
    }

    private void Update()
    {
        /*
        for (int i = 0; i < GameSystem.instance.combination_list.Count; i++)
        {
            recipe_box[i].GetComponent<RecipeBoxEvent>().image.sprite = Resources.Load<Sprite>("Item2D/" + GameSystem.instance.combination_list[i]["name"]);
        }*/
    }

    public void recipe_click(GameObject gameObject)
    {
        recipe_choose = gameObject.name;

        Image new_image = gameObject.GetComponent<RecipeBoxEvent>().image;
        image.sprite = new_image.sprite;

        name.text = GameSystem.instance.item_search(gameObject.name, "name_ko");
        string use = null;
        for (int i = 0; i < GameSystem.instance.combination_list.Count; i++)
        {
            if (GameSystem.instance.combination_list[i]["name"] == recipe_choose)
            {
                seach_num = i;
                break;
            }
        }
        for (int i = 0; i < System.Convert.ToInt32(GameSystem.instance.combination_list[seach_num]["item_num"]); i++)
        {
            if (use != null)
                use += ", ";
            use += GameSystem.instance.item_search(GameSystem.instance.combination_list[seach_num]["name" + (i + 1)], "name_ko");
        }
        explane.text = "아이템 설명 | " + GameSystem.instance.item_search(gameObject.name, "explanation_ko") + "\n\n필요한 재료 | " + use;
    }

    public void select_click()
    {
        for (int i = 0; i < item_box.Count; i++) // 있던 객체 삭제
        {
            Destroy(item_box[i]);
        }
        item_box.Clear();

        for (int i = 0; i < GameSystem.instance.combination_list.Count; i++)
        {
            if (GameSystem.instance.combination_list[i]["name"] == recipe_choose)
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

        select.SetActive(false);
        combin.SetActive(true);
        type = 1;

        succses.SetActive(false);
        fail.SetActive(false);
    }

    public void return_click()
    {
        if (type == 1)
        {
            select.SetActive(true);
            combin.SetActive(false);
            type = 0;
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
            Eat_system.instance.eat_item(recipe_choose);

            StartCoroutine(GameObject.Find("BigItem_get").GetComponent<BigItem_get>().
                        active_on(recipe_choose, false));

            for (int i = 1; i <= System.Convert.ToInt32(GameSystem.instance.combination_list[seach_num]["item_num"]); i++)
            {
                GameSystem.instance.item_num[GameSystem.instance.combination_list[seach_num]["name" + i]] -= System.Convert.ToInt32(GameSystem.instance.combination_list[seach_num]["num" + i]);
                if (GameSystem.instance.item_num[GameSystem.instance.combination_list[seach_num]["name" + i]] == 0)
                    GameSystem.instance.item_time.Remove(GameSystem.instance.combination_list[seach_num]["name" + i]);
            }

            succses.SetActive(true);
            fail.SetActive(false);
        }
        else
        {
            succses.SetActive(false);
            fail.SetActive(true);
        }
    }
}
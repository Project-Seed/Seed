﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public GameObject note;

    public GameObject viewport;
    public GameObject content;
    public bool quick_bool = false;
    public GameObject quick_bg;
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

    public string sentence;

    
    private void Awake()
    {
        for (int i=0; i<40; i++)
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
        for (int i = 0; i < 40; i++)
        {
            if (i < GameSystem.instance.item_time.Count)
            {
                item_box[i].GetComponent<Inventory_box>().image.sprite = Resources.Load<Sprite>("Item2D/" + GameSystem.instance.item_time[i]);
                item_box[i].GetComponentInChildren<Text>().text = "x" + GameSystem.instance.item_num[GameSystem.instance.item_time[i]].ToString();
            }
            else
            {
                item_box[i].GetComponent<Inventory_box>().image.sprite = alpha;
                item_box[i].GetComponentInChildren<Text>().text = "";
            }
        }

        if (item_choose != null)
        {
            StopCoroutine(TypeSentence());
            sentence = GameSystem.instance.item_search(item_choose, "explanation_ko");
            StartCoroutine(TypeSentence());
        }
    }

    public void Inventory_click(GameObject gameObject) // 인벤토리 내용물 클릭
    {
        for (int i = 0; i < 40; i++)
        {
            item_box[i].GetComponent<Inventory_box>().choose.SetActive(false);
        }
        gameObject.GetComponent<Inventory_box>().choose.SetActive(true);

        string item_name = gameObject.name;
        item_name = item_name.Substring(14, item_name.Length - 14);
        int item_int = System.Convert.ToInt32(item_name);

        try 
        { item_choose = GameSystem.instance.item_time[item_int]; }
        catch 
        {}

        if (item_int < GameSystem.instance.item_time.Count)
        {
            if (item_choose != null)
            {
                StopCoroutine(TypeSentence());
                sentence = GameSystem.instance.item_search(item_choose, "explanation_ko");
                StartCoroutine(TypeSentence());
            }

            item_names.text = GameSystem.instance.item_search(item_choose, "name_ko");
            item_image.sprite = gameObject.GetComponent<Inventory_box>().image.sprite;

            if (GameSystem.instance.item_search(item_choose, "category") == "consumable")
            {
                quick_bool = false;

                spand_button.SetActive(true);
            }
            else
            {
                quick_bool = false;

                spand_button.SetActive(false);
            }
        }
    }

    public void spand()
    {
        if (GameSystem.instance.item_num[item_choose] >= 1)
        {
            switch (GameSystem.instance.item_search(item_choose, "name"))
            {
                case "medi_01":
                    if (PlayerState.instance.hp + 2 < PlayerState.instance.max_hp)
                        State.instance.hp_up(2);
                    else
                        State.instance.hp_up(PlayerState.instance.max_hp - PlayerState.instance.hp);

                    GameSystem.instance.item_num[item_choose]--;

                    if (GameSystem.instance.item_num[item_choose] == 0)
                        GameSystem.instance.item_time.Remove(item_choose); 
                    break;

                case "medi_02":
                    if (PlayerState.instance.hp + 4 < PlayerState.instance.max_hp)
                        State.instance.hp_up(4);
                    else
                        State.instance.hp_up(PlayerState.instance.max_hp - PlayerState.instance.hp);

                    GameSystem.instance.item_num[item_choose]--;

                    if (GameSystem.instance.item_num[item_choose] == 0)
                        GameSystem.instance.item_time.Remove(item_choose);
                    break;

                case "medi_03":
                    if (PlayerState.instance.hp + 6 < PlayerState.instance.max_hp)
                        State.instance.hp_up(6);
                    else
                        State.instance.hp_up(PlayerState.instance.max_hp - PlayerState.instance.hp);

                    GameSystem.instance.item_num[item_choose]--;

                    if (GameSystem.instance.item_num[item_choose] == 0)
                        GameSystem.instance.item_time.Remove(item_choose);
                    break;

                case "mini_latter":
                    note.SetActive(false);

                    InputManager.instance.click_mod = 1;
                    Quest_clear_system.instance.clear_trigger[8]++;
                    Instantiate(Resources.Load<GameObject>("Tutorial/Mini_latter"), GameObject.Find("Canvas").transform);
                    break;
            }
        }
    }

    IEnumerator TypeSentence() // 한글자씩 출력
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

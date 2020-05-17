using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inven_quick : MonoBehaviour
{
    public string item_name; // 현재 선택된 아이템 이름
    public int num = 0; // 선택된 슬롯 번호(0~4)

    public List<Sprite> slot_on_sp; // 선택된 슬롯
    public List<Sprite> slot_off_sp; // 안선택된 슬롯

    public List<GameObject> slot; // 슬롯
    public List<Image> slot_image; // 슬롯에 아이템 이미지
    public List<Text> slot_text; // 슬롯에 아이템 갯수
    public List<string> item_names; // 아이템 이름들
    public List<int> item_active; // 아이템 등록되면 활성화
    public List<Image> inven_slot_image; // 인벤쪽 이미지
    public List<Text> inven_slot_text; // 인벤쪽 텍스트

<<<<<<< HEAD
    void Update()
    {
=======
    private void Awake()
    {
        num = 0;
        slot[0].GetComponent<Image>().sprite = slot_on_sp[0];
        slot[1].GetComponent<Image>().sprite = slot_off_sp[1];
        slot[2].GetComponent<Image>().sprite = slot_off_sp[2];
        slot[3].GetComponent<Image>().sprite = slot_off_sp[3];
        slot[4].GetComponent<Image>().sprite = slot_off_sp[4];

        item_name = item_names[num];
    }

    void Update()
    {
        for (int i = 0; i <= 4; i++)
        {
            if (item_active[i] == 1)
            {
                slot_text[i].text = GameSystem.instance.item_num[item_names[i]].ToString();
                inven_slot_text[i].text = GameSystem.instance.item_num[item_names[i]].ToString();
            }
        }
>>>>>>> 83c4f9dbb54b6074358e73bf1ba2b18c52c80cb3


        if(Input.GetKeyDown(KeyCode.Alpha1) && InputManager.instance.click_mod == 0)
        {
            num = 0;
            slot[0].GetComponent<Image>().sprite = slot_on_sp[0];
            slot[1].GetComponent<Image>().sprite = slot_off_sp[1];
            slot[2].GetComponent<Image>().sprite = slot_off_sp[2];
            slot[3].GetComponent<Image>().sprite = slot_off_sp[3];
            slot[4].GetComponent<Image>().sprite = slot_off_sp[4];

            item_name = item_names[num];
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2) && InputManager.instance.click_mod == 0)
        {
            num = 1;
            slot[0].GetComponent<Image>().sprite = slot_off_sp[0];
            slot[1].GetComponent<Image>().sprite = slot_on_sp[1];
            slot[2].GetComponent<Image>().sprite = slot_off_sp[2];
            slot[3].GetComponent<Image>().sprite = slot_off_sp[3];
            slot[4].GetComponent<Image>().sprite = slot_off_sp[4];

            item_name = item_names[num];
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3) && InputManager.instance.click_mod == 0)
        {
            num = 2;
            slot[0].GetComponent<Image>().sprite = slot_off_sp[0];
            slot[1].GetComponent<Image>().sprite = slot_off_sp[1];
            slot[2].GetComponent<Image>().sprite = slot_on_sp[2];
            slot[3].GetComponent<Image>().sprite = slot_off_sp[3];
            slot[4].GetComponent<Image>().sprite = slot_off_sp[4];

            item_name = item_names[num];
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4) && InputManager.instance.click_mod == 0)
        {
            num = 3;
            slot[0].GetComponent<Image>().sprite = slot_off_sp[0];
            slot[1].GetComponent<Image>().sprite = slot_off_sp[1];
            slot[2].GetComponent<Image>().sprite = slot_off_sp[2];
            slot[3].GetComponent<Image>().sprite = slot_on_sp[3];
            slot[4].GetComponent<Image>().sprite = slot_off_sp[4];

            item_name = item_names[num];
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5) && InputManager.instance.click_mod == 0)
        {
            num = 4;
            slot[0].GetComponent<Image>().sprite = slot_off_sp[0];
            slot[1].GetComponent<Image>().sprite = slot_off_sp[1];
            slot[2].GetComponent<Image>().sprite = slot_off_sp[2];
            slot[3].GetComponent<Image>().sprite = slot_off_sp[3];
            slot[4].GetComponent<Image>().sprite = slot_on_sp[4];

            item_name = item_names[num];
        }
    }

    public void item_set(int num, string name)
    {
        item_names[num] = name;
        item_active[num] = 1;
        slot_image[num].sprite = Resources.Load<Sprite>("Item2D/" + item_names[num]);
        inven_slot_image[num].sprite = Resources.Load<Sprite>("Item2D/" + item_names[num]);
<<<<<<< HEAD
=======

        item_name = item_names[num];
>>>>>>> 83c4f9dbb54b6074358e73bf1ba2b18c52c80cb3
    }
}

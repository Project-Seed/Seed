using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Quick_slot_new : MonoBehaviour
{
    public List<GameObject> slot;
    public List<Image> image_color;
    public List<Text> item_num;

    public List<Image> image_seed;
    public List<Text> key;

    public Sprite black;
    public Sprite gray;
    public Sprite yellow;

    public Image image;
    public Text text_num;

    public Image main_color;
    public Sprite black_main;
    public Sprite red_main;

    bool open = false; // 열면 트루
    bool close_tri = false; // 닫기중 트루
    float open_time = 0; // 열리고 닫치기 까지 남은 시간

    public int choose_num = 1;
    public string choose_item;

    void Start()
    {
        for(int j=0; j<7; j++)
        {
            slot[j].transform.Translate(-115.5f, 0, 0);

            image_color[j].color = new Color(1, 1, 1, 0);
            image_seed[j].color = new Color(1, 1, 1, 0);

            item_num[j].color = new Color(1, 1, 1, 0);
            key[j].color = new Color(1, 1, 1, 0);
        }
    }

    void Update()
    {
        if (open && open_time > 0)
            open_time -= Time.deltaTime;
        else if(open && open_time <= 0 && close_tri == false)
            StartCoroutine(close_ani());

        if(open)
            main_color.sprite = red_main;
        else
            main_color.sprite = black_main;

        if (GameSystem.instance.GetModeNum() == 0)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1) && close_tri == false)
            {
                if (open == false)
                    StartCoroutine(open_ani());

                open_time = 2;
                choose_num = 1;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2) && close_tri == false)
            {
                if (open == false)
                    StartCoroutine(open_ani());

                open_time = 2;
                choose_num = 2;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3) && close_tri == false)
            {
                if (open == false)
                    StartCoroutine(open_ani());

                open_time = 2;
                choose_num = 3;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha4) && close_tri == false)
            {
                if (open == false)
                    StartCoroutine(open_ani());

                open_time = 2;
                choose_num = 4;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha5) && close_tri == false)
            {
                if (open == false)
                    StartCoroutine(open_ani());

                open_time = 2;
                choose_num = 5;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha6) && close_tri == false)
            {
                if (open == false)
                    StartCoroutine(open_ani());

                open_time = 2;
                choose_num = 6;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha7))
            {
                if (open == false && close_tri == false)
                    StartCoroutine(open_ani());

                open_time = 2;
                choose_num = 7;
            }
        }

        item_num[0].text = GameSystem.instance.item_num["blue_seed"].ToString();
        item_num[1].text = GameSystem.instance.item_num["brown_seed"].ToString();
        item_num[2].text = GameSystem.instance.item_num["green_seed"].ToString();
        item_num[3].text = GameSystem.instance.item_num["red_seed"].ToString();
        item_num[4].text = GameSystem.instance.item_num["yellow_seed"].ToString();
        item_num[5].text = GameSystem.instance.item_num["white_seed"].ToString();
        item_num[6].text = GameSystem.instance.item_num["purple_seed"].ToString();

        switch(choose_num)
        {
            case 1: choose_item = "blue_seed"; break;
            case 2: choose_item = "brown_seed"; break;
            case 3: choose_item = "green_seed"; break;
            case 4: choose_item = "red_seed"; break;
            case 5: choose_item = "yellow_seed"; break;
            case 6: choose_item = "white_seed"; break;
            case 7: choose_item = "purple_seed"; break;
        }

        for (int i=0; i<7; i++)
        {
            if (choose_num == i+1)
            {
                image_color[i].sprite = yellow;
                
                image.sprite = image_seed[i].sprite;
                text_num.text = item_num[i].text;
            }
            else
            {
                if(item_num[i].text == "0")
                    image_color[i].sprite = black;
                else
                    image_color[i].sprite = gray;
            }
        }
    }

    IEnumerator open_ani() 
    {
        open = true;
        
        int j = 0;

        for (int i = 0; i < 35; i++)
        {
            switch (i)
            {
                case 5:
                    j = 1;
                    break;
                case 10:
                    j = 2;
                    break;
                case 15:
                    j = 3;
                    break;
                case 20:
                    j = 4;
                    break;
                case 25:
                    j = 5;
                    break;
                case 30:
                    j = 6;
                    break;
            }

            int k = i - (j * 5) + 1;

            slot[j].transform.Translate(22f, 0, 0);

            image_color[j].color = new Color(1, 1, 1, k / 5f);
            image_seed[j].color = new Color(1, 1, 1, k / 5f);

            item_num[j].color = new Color(1, 1, 1, k / 5f);
            key[j].color = new Color(1, 1, 1, k / 5f);

            yield return new WaitForSeconds(0.0003f);
        }
    }

    IEnumerator close_ani()
    {
        close_tri = true;

        int j = 6;

        for (int i = 35; i >= 1; i--)
        {
            switch (i)
            {
                case 5:
                    j = 0;
                    break;
                case 10:
                    j = 1;
                    break;
                case 15:
                    j = 2;
                    break;
                case 20:
                    j = 3;
                    break;
                case 25:
                    j = 4;
                    break;
                case 30:
                    j = 5;
                    break;
            }

            int k = i - (j * 5) - 1;

            slot[j].transform.Translate(-22f, 0, 0);

            image_color[j].color = new Color(1, 1, 1, k / 5f);
            image_seed[j].color = new Color(1, 1, 1, k / 5f);

            item_num[j].color = new Color(1, 1, 1, k / 5f);
            key[j].color = new Color(1, 1, 1, k / 5f);

            yield return new WaitForSeconds(0.001f);
        }
        open = false;
        close_tri = false;
    }
}

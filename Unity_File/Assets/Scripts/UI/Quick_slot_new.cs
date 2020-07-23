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

    int choose_num = 1;

    void Start()
    {
        for(int j=0; j<7; j++)
        {
            slot[j].transform.Translate(-115.5f * (j + 1), 0, 0);

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

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (open == false && close_tri == false)
                StartCoroutine(open_ani());

            open_time = 2;
            choose_num = 1;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (open == false && close_tri == false)
                StartCoroutine(open_ani());

            open_time = 2;
            choose_num = 2;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            if (open == false && close_tri == false)
                StartCoroutine(open_ani());

            open_time = 2;
            choose_num = 3;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            if (open == false && close_tri == false)
                StartCoroutine(open_ani());

            open_time = 2;
            choose_num = 4;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            if (open == false && close_tri == false)
                StartCoroutine(open_ani());

            open_time = 2;
            choose_num = 5;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            if (open == false && close_tri == false)
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

        item_num[0].text = GameSystem.instance.item_num["blue_seed"].ToString();
        item_num[1].text = GameSystem.instance.item_num["brown_seed"].ToString();
        item_num[2].text = GameSystem.instance.item_num["green_seed"].ToString();
        item_num[3].text = GameSystem.instance.item_num["red_seed"].ToString();
        item_num[4].text = GameSystem.instance.item_num["yellow_seed"].ToString();
        item_num[5].text = GameSystem.instance.item_num["white_seed"].ToString();
        item_num[6].text = GameSystem.instance.item_num["purple_seed"].ToString();

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
        for (int i = 0; i <= 20; i++)
        {
            for (int j = 0; j < 7; j++)
            {
                slot[j].transform.Translate(5.5f * (j + 1), 0, 0);

                image_color[j].color = new Color(1, 1, 1, i / 20f);
                image_seed[j].color = new Color(1, 1, 1, i / 20f);

                item_num[j].color = new Color(1, 1, 1, i / 20f);
                key[j].color = new Color(1, 1, 1, i / 20f);
            }

            yield return new WaitForSeconds(0.01f);
        }
    }

    IEnumerator close_ani()
    {
        close_tri = true;
        for (int i = 0; i <= 20; i++)
        {
            for (int j = 0; j < 7; j++)
            {
                slot[j].transform.Translate(-5.5f * (j + 1), 0, 0);

                image_color[j].color = new Color(1, 1, 1, (20 - i) / 20f);
                image_seed[j].color = new Color(1, 1, 1, (20 - i) / 20f);

                item_num[j].color = new Color(1, 1, 1, (20 - i) / 20f);
                key[j].color = new Color(1, 1, 1, (20 - i) / 20f);
            }

            yield return new WaitForSeconds(0.01f);
        }
        open = false;
        close_tri = false;
    }
}

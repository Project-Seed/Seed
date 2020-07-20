using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Note : MonoBehaviour
{
    public GameObject illustrated;
    public GameObject diary;
    public GameObject map;
    public GameObject quest;
    public GameObject inven;

    public List<Image> icon_image;
    public List<Sprite> icon_on_sp;
    public List<Sprite> icon_off_sp;


    private void Awake()
    {
        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        InputManager.instance.game_stop();
    }
    private void OnDisable()
    {
        InputManager.instance.game_start();
    }

    public void On_illustrated()
    {
        illustrated.SetActive(true);
        diary.SetActive(false);
        map.SetActive(false);
        quest.SetActive(false);
        inven.SetActive(false);

        icon_image[0].sprite = icon_on_sp[0];
        icon_image[1].sprite = icon_off_sp[1];
        icon_image[2].sprite = icon_off_sp[2];
        icon_image[3].sprite = icon_off_sp[3];
        icon_image[4].sprite = icon_off_sp[4];
    }

    public void On_diary()
    {
        illustrated.SetActive(false);
        diary.SetActive(true);
        map.SetActive(false);
        quest.SetActive(false);
        inven.SetActive(false);

        icon_image[0].sprite = icon_off_sp[0];
        icon_image[1].sprite = icon_on_sp[1];
        icon_image[2].sprite = icon_off_sp[2];
        icon_image[3].sprite = icon_off_sp[3];
        icon_image[4].sprite = icon_off_sp[4];
    }

    public void On_map()
    {
        illustrated.SetActive(false);
        diary.SetActive(false);
        map.SetActive(true);
        quest.SetActive(false);
        inven.SetActive(false);

        icon_image[0].sprite = icon_off_sp[0];
        icon_image[1].sprite = icon_off_sp[1];
        icon_image[2].sprite = icon_on_sp[2];
        icon_image[3].sprite = icon_off_sp[3];
        icon_image[4].sprite = icon_off_sp[4];
    }

    public void On_quest()
    {
        illustrated.SetActive(false);
        diary.SetActive(false);
        map.SetActive(false);
        quest.SetActive(true);
        inven.SetActive(false);

        icon_image[0].sprite = icon_off_sp[0];
        icon_image[1].sprite = icon_off_sp[1];
        icon_image[2].sprite = icon_off_sp[2];
        icon_image[3].sprite = icon_on_sp[3];
        icon_image[4].sprite = icon_off_sp[4];
    }

    public void On_inven()
    {
        illustrated.SetActive(false);
        diary.SetActive(false);
        map.SetActive(false);
        quest.SetActive(false);
        inven.SetActive(true);

        icon_image[0].sprite = icon_off_sp[0];
        icon_image[1].sprite = icon_off_sp[1];
        icon_image[2].sprite = icon_off_sp[2];
        icon_image[3].sprite = icon_off_sp[3];
        icon_image[4].sprite = icon_on_sp[4];
    }

    public void close_note()
    {
        gameObject.SetActive(false);
    }
}

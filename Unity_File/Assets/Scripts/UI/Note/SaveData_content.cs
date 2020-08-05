using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class SaveData_content : MonoBehaviour
{
    public int click_num;

    public Text save;
    public Text time;
    public Text quest;

    public GameObject save_ob;
    public GameObject load_ob;

    public List<SaveData> saveDatas;

    bool key_ok = true;

    public bool die = false;
    public Game_over game_Over;

    public void save_game()
    {
        key_ok = true;
        save_ob.SetActive(false);

        GameSystem.instance.save_gema(click_num);

        for (int i = 0; i < 6; i++)
        {
            saveDatas[i].open();
        }
        saveDatas[click_num - 1].click();
    }

    public void load_game()
    {
        key_ok = true;
        load_ob.SetActive(false);

        if (die)
            game_Over.restart();

        GameSystem.instance.load_game(click_num);
    }

    public void click()
    {
        for (int i = 0; i < 6; i++)
        {
            saveDatas[i].choose.SetActive(false);
        }
    }

    public void yes()
    {
        if (save_ob.activeSelf == true)
            save_game();
        else if (load_ob.activeSelf == true)
            load_game();
    }

    public void no()
    {
        save_ob.SetActive(false);
        load_ob.SetActive(false);
        key_ok = true;
    }

    public void saves()
    {
        if (key_ok == true)
        {
            save_ob.SetActive(true);
            key_ok = false;
        }
    }

    public void loads()
    {
        if (key_ok == true)
        {
            string strFile = GameSystem.instance.save_path + "./save_data" + click_num.ToString() + ".xml";
            FileInfo fileInfo = new FileInfo(strFile); // 파일 있는지 체크

            if (fileInfo.Exists)
            {
                load_ob.SetActive(true);
                key_ok = false;
            }
        }
    }
}

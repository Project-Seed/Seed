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

    public void save_game()
    {
        key_ok = true;
        save_ob.SetActive(false);

        GameSystem.instance.save_gema(click_num);

        for(int i=0; i<6; i++)
        {
            saveDatas[i].open();
        }
        saveDatas[click_num - 1].click();
    }

    public void load_game()
    {
        key_ok = true;
        load_ob.SetActive(false);

        GameSystem.instance.load_game(click_num);
    }

    public void close()
    {
        save_ob.SetActive(false);
        load_ob.SetActive(false);
        key_ok = true;
    }

    public void click()
    {
        for (int i = 0; i < 6; i++)
        {
            saveDatas[i].choose.SetActive(false);
        }
    }

    private void Update()
    {
        if (key_ok == true)
        {
            if (Input.GetKeyDown(KeyCode.Z))
            {
                save_ob.SetActive(true);
                key_ok = false;
            }
            else if (Input.GetKeyDown(KeyCode.X))
            {
                string strFile = GameSystem.instance.save_path + "./save_data" + click_num.ToString() + ".xml";
                FileInfo fileInfo = new FileInfo(strFile); // 파일 있는지 체크

                if (fileInfo.Exists)
                {
                    load_ob.SetActive(true);
                    key_ok = false;
                }
                else
                {

                }
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Z) && save_ob.activeSelf == true)
                save_game();
            else if (Input.GetKeyDown(KeyCode.Z) && load_ob.activeSelf == true)
                load_game();
            else if (Input.GetKeyDown(KeyCode.X))
                close();
        }
    }
}

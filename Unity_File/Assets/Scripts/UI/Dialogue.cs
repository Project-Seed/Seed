using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dialogue : MonoBehaviour
{
    public GameObject npc_name;
    public GameObject talk_guide;
    public GameObject dialogue_box;

    public Text name_text;
    public GameObject quest_state;
    public Image quest_image;

    public Sprite quest_start;
    public Sprite quest_ing;
    public Sprite quest_end;

    bool quest_bool = false;
    bool talk_bool = false;


    public Camera cameras;
    GameObject npc_ob;
    GameObject name_position;

    void Start()
    {

    }

    void Update()
    {
        if (quest_bool == true)
        {
            Vector3 guide_pos = cameras.WorldToScreenPoint(npc_ob.transform.position);
            Vector3 name_pos = cameras.WorldToScreenPoint(name_position.transform.position);

            npc_name.transform.position = new Vector3(name_pos.x, name_pos.y, npc_name.transform.position.z);
            talk_guide.transform.position = new Vector3(guide_pos.x, guide_pos.y, talk_guide.transform.position.z);

            if (Input.GetKeyDown(KeyCode.H))
            {
                if (dialogue_box.activeSelf == false)
                {
                    dialogue_box.SetActive(true);
                }
            }
        }
    }

    public void quest_on(GameObject npc_obs, GameObject name_positions, string name)
    {
        npc_name.SetActive(true);
        talk_guide.SetActive(true);

        npc_ob = npc_obs;
        name_position = name_positions;
        name_text.text = name;

        quest_bool = true;
    }

    public void quest_off()
    {
        npc_name.SetActive(false);
        talk_guide.SetActive(false);

        quest_bool = false;
    }

    public void talk_box()
    {

    }
}

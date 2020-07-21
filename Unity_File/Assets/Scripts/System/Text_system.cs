using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class Text_system : MonoBehaviour
{
    public static List<Dictionary<string, string>> TextList; //


    public Text Name_Text;
    public Text Messge_Text;

    public GameObject character1;
    public GameObject character2;

    public string Player_name = "시드";

    public GameObject choose1;
    public GameObject choose2;
    public GameObject choose3;

    public Text choose1_text;
    public Text choose2_text;
    public Text choose3_text;

    private string Name_q;
    private string Messge_q;

    private bool next_end = false;

    int start_text_num = -1;
    int now_text_num = -1;

    private void Awake()
    {
        TextList = CSV_Reader.Read("Text");
    }

    public void StartDialogue(int text_num) // 처음 대화 시작
    {
        Name_q = null;
        Messge_q = null;

        for(int i=0; i< TextList.Count; i++)
        {
            if(TextList[i]["scene"] == text_num.ToString())
            {
                start_text_num = i;
                break;
            }
        }

        now_text_num = start_text_num;
        next_end = false;

        Next_text();
    }

    public void Next_text()
    {
        if(next_end == true)
        {
            if (TextList[now_text_num]["quest_num"] != "")
            {
                GameSystem.instance.quest_state[int.Parse(TextList[now_text_num]["quest_num"])]++;

                if (GameSystem.instance.quest_state[int.Parse(TextList[now_text_num]["quest_num"])] == 3)
                {
                    Quest_clear_system.instance.clear_reward(int.Parse(TextList[now_text_num]["quest_num"]));

                    GameObject.Find("Quest_quick").GetComponent<Quest_quick>().start_co(GameSystem.instance.quest_list[int.Parse(TextList[now_text_num]["quest_num"]) - 1]["title"], true);
                }
                else if (GameSystem.instance.quest_state[int.Parse(TextList[now_text_num]["quest_num"])] == 1)
                {
                    GameObject.Find("Quest_quick").GetComponent<Quest_quick>().start_co(GameSystem.instance.quest_list[int.Parse(TextList[now_text_num]["quest_num"]) - 1]["title"], false);
                }
            }
            gameObject.GetComponent<Dialogue>().talk_end();
        }
        else if (TextList[now_text_num]["type"] == "talk" || TextList[now_text_num]["type"] == "end")
        {
            if (TextList[now_text_num]["name"] == "user")
                Name_q = Player_name;
            else
                Name_q = TextList[now_text_num]["name"];

            Messge_q = TextList[now_text_num]["text"];


            Name_Text.text = Name_q;

            StopAllCoroutines();
            StartCoroutine(TypeSentence(Messge_q));


            if (TextList[now_text_num]["type"] == "end")
                next_end = true;
            else
                now_text_num = int.Parse(TextList[now_text_num]["next_num"]) - 1;
        }
        else if(TextList[now_text_num]["type"] == "choice")
        {
            Name_Text.text = Name_q;

            if (int.Parse(TextList[now_text_num]["choice_num"]) == 2)
            {
                choose1.SetActive(true);
                choose2.SetActive(true);

                choose1_text.text = TextList[now_text_num]["choice1"];
                choose2_text.text = TextList[now_text_num]["choice2"];
            }
            else if (int.Parse(TextList[now_text_num]["choice_num"]) == 3)
            {
                choose1.SetActive(true);
                choose2.SetActive(true);
                choose3.SetActive(true);

                choose1_text.text = TextList[now_text_num]["choice1"];
                choose2_text.text = TextList[now_text_num]["choice2"];
                choose2_text.text = TextList[now_text_num]["choice3"];
            }
        }
    }

    IEnumerator TypeSentence(string sentence) // 한글자씩 출력
    {
        Messge_Text.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            if(letter == '$')
                Messge_Text.text += "\n";
            else if (letter == '@')
                Messge_Text.text += ",";
            else
                Messge_Text.text += letter;
            yield return null;
        }
    }

    public void choose_select(int select)
    {
        if(select == 1)
            now_text_num = int.Parse(TextList[now_text_num]["next_num1"]) - 1;
        else if (select == 2)
            now_text_num = int.Parse(TextList[now_text_num]["next_num2"]) - 1;
        else if (select == 3)
            now_text_num = int.Parse(TextList[now_text_num]["next_num3"]) - 1;

        choose1.SetActive(false);
        choose2.SetActive(false);
        choose3.SetActive(false);

        Next_text();
    }
}

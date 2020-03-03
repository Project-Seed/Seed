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

    private string Name_q;
    private string Messge_q;

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

        Next_text();
    }

    public void Next_text()
    {
        if ((string)TextList[now_text_num]["name"] == "user")
            Name_q = Player_name;
        else
            Name_q = (string)TextList[now_text_num]["name"];

        Messge_q = (string)TextList[now_text_num]["text"];



        Name_Text.text = Name_q;

        StopAllCoroutines();
        StartCoroutine(TypeSentence(Messge_q));



        if (TextList[now_text_num]["type"] == "end")
            gameObject.SetActive(false);
        else
            now_text_num++; // 다음 숫자로 이동으로 고쳐야됨
    }

    IEnumerator TypeSentence(string sentence) // 한글자씩 출력
    {
        Messge_Text.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            Messge_Text.text += letter;
            yield return null;
        }
    }
}

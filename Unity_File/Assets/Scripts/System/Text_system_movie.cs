using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class Text_system_movie : MonoBehaviour
{
    public Tutorial tutorial;

    public List<string> TextList; //

    public Text Messge_Text;

    private string Messge_q;

    int num;


    private void Awake()
    {
        //TextList = CSV_Reader.Read("Text");
    }

    public void StartDialogue(int text_num) // 처음 대화 시작
    {
        num = text_num;
        Messge_q = null;

        Next_text();
    }

    public void Next_text()
    {
            Messge_q = TextList[num];
            num++;

            StopAllCoroutines();
            StartCoroutine(TypeSentence(Messge_q));
        
    }

    IEnumerator TypeSentence(string sentence) // 한글자씩 출력
    {
        Messge_Text.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            if(letter == '$')
                Messge_Text.text += "\n";
            else
                Messge_Text.text += letter;
            yield return null;
        }
    }
}

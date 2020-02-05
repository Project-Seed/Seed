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

    public string Player_name;

    private Queue<string> Name_q;
    private Queue<string> Messge_q;

    private void Awake()
    {
        TextList = CSV_Reader.Read("Text");
    }

    void Start()
    {
        Name_q = new Queue<string>();
        Messge_q = new Queue<string>();

        Player_name = "시드";

        StartDialogue(); // 시작해버림
    }

    public void StartDialogue() // 처음 대화 시작
    {
        Name_q.Clear();
        Messge_q.Clear();


        for (int i = 0; i < 5; i++)
        {
            if ((string)TextList[i]["name"] == "user")
                Name_q.Enqueue(Player_name);
            else
                Name_q.Enqueue((string)TextList[i]["name"]);

            Messge_q.Enqueue((string)TextList[i]["text"]);
        }


        DisplayNextSentence();
    }

    public void DisplayNextSentence() // 대화 진행
    {
        if (Name_q.Count == 0)
        {
            EndDialogue();
            return;
        }

        string Name_eq = Name_q.Dequeue();
        string Messge_wq = Messge_q.Dequeue();

        if (Name_eq == "choice")
        {

        }
        else
        {
            Name_Text.text = Name_eq;

            StopAllCoroutines();
            StartCoroutine(TypeSentence(Messge_wq));
        }
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

    void EndDialogue()
    {
        Name_Text.text = "대화 종료";
        Messge_Text.text = "대화 종료";
    }
}

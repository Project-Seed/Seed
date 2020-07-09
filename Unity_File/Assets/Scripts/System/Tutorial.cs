using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    public Dialogue dialogue;
    public Text_system_movie movie;
    public GameObject movie_obj;

    public void tutorial(int num)
    {
        switch(num)
        {
            case 1:
                movie_obj.SetActive(true);
                movie.StartDialogue(0);  
                break;

            case 2:
            case 3:
                movie.Next_text();
                break;

            case 4:
                movie_obj.SetActive(false);
                dialogue.solo_talk(16);
                GameObject.Find("M_Shopa01").GetComponent<BoxCollider>().enabled = true;
                break;
        }
    }
}

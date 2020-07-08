using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    public Dialogue dialogue;
    public Text_system_movie movie;

    void Start()
    {
        StartCoroutine(start_tutorial());
    }

    IEnumerator start_tutorial()
    {
        yield return new WaitForSeconds(1f);

        tutorial(1);
    }

    public void tutorial(int num)
    {
        switch(num)
        {
            case 1:
                movie.StartDialogue(0);  
                break;

            case 2:
                dialogue.solo_talk(16);
                break;
        }
    }
}

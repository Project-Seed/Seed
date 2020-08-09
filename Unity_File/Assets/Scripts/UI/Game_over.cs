using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game_over : MonoBehaviour
{
    public List<Image> image;
    public List<Text> texts;
    public GameObject load_choose;

    private void OnEnable()
    {
        for (int i = 0; i < image.Count; i++)
        {
            image[i].color = new Color(1, 1, 1, 0);
        }
        for (int i = 0; i < texts.Count; i++)
        {
            texts[i].color = new Color(1, 1, 1, 0);
        }
    }

    public void image_on()
    {
        StartCoroutine(on());
    }

    IEnumerator on()
    {
        for (int i = 0; i <= 100; i++)
        {
            for (int j = 0; j < image.Count; j++)
            {
                image[j].color = new Color(1, 1, 1, i / 100f);
            }
            for (int j = 0; j < texts.Count; j++)
            {
                texts[j].color = new Color(1, 1, 1, i / 100f);
            }
            yield return new WaitForSeconds(0.01f);
        }
    }

    public void home()
    {
        LoadingSceneManager.LoadScene("Title_Scene");
    }

    public void load()
    {
        load_choose.SetActive(true);
    }

    public void restart()
    {
        load_choose.SetActive(false);
        gameObject.SetActive(false);

        PlayerState.instance.reborn();
        //State.instance.restart();
        InputManager.instance.click_mod = 0;
    }    
}

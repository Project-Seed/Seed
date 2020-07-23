using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BigItem_get : MonoBehaviour
{
    public GameObject back_ob;
    public Image background;
    public Text name_text;
    public Text explanation_text;
    public Image image;
    public GameObject kira1;
    public GameObject kira2;

    private void Awake()
    {
        background.GetComponent<Button>().enabled = false;

        background.color = new Color(0, 0, 0, 0);
        name_text.color = new Color(1, 1, 1, 0);
        explanation_text.color = new Color(1, 1, 1, 0);
        image.color = new Color(1, 1, 1, 0);
    }

    private void Update()
    {
        kira1.transform.Rotate(0, 0, 150 * Time.deltaTime);
        kira2.transform.Rotate(0, 0, -150 * Time.deltaTime);
    }

    public IEnumerator active_on(string names)
    {
        background.color = new Color(0, 0, 0, 0);
        name_text.color = new Color(1, 1, 1, 0);
        explanation_text.color = new Color(1, 1, 1, 0);
        image.color = new Color(1, 1, 1, 0);

        name_text.text = GameSystem.instance.item_search(names, "name_ko");
        explanation_text.text = GameSystem.instance.item_search(names, "explanation_ko");
        image.sprite = Resources.Load<Sprite>("Item2D/" + names);

        back_ob.SetActive(true);

        for (int i = 0; i <= 30; i++)
        {
            background.color = new Color(1, 1, 1, i / 30f);
            name_text.color = new Color(1, 1, 1, i / 30f);
            explanation_text.color = new Color(1, 1, 1, i / 30f);
            image.color = new Color(1, 1, 1, i / 30f);

            if (i <= 20)
                background.GetComponent<RectTransform>().anchoredPosition = new Vector2(0f, -70f + (i / 20f * 100));
            else
                background.GetComponent<RectTransform>().anchoredPosition = new Vector2(0f, 30f + ((i - 20) / 20f * -30));

            yield return new WaitForSeconds(0.01f);
        }
 
        background.GetComponent<Button>().enabled = true;
    }

    public void click()
    {
        StartCoroutine(active_off());

        background.GetComponent<Button>().enabled = false;
    }

    public IEnumerator active_off()
    {
        for (int i = 0; i <= 20; i++)
        {
            background.color = new Color(1, 1, 1, (20 - i) / 20f);
            name_text.color = new Color(1, 1, 1, (20 - i) / 20f);
            explanation_text.color = new Color(1, 1, 1, (20 - i) / 20f);
            image.color = new Color(1, 1, 1, (20 - i) / 20f);

            yield return new WaitForSeconds(0.01f);
        }

        back_ob.SetActive(false);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpBar : MonoBehaviour
{
    public Slider hpbar;
    public Text hptext;

    // Start is called before the first frame update
    void Awake()
    {
        hpbar.maxValue = GameSystem.instance.max_hp;
        hpbar.value = GameSystem.instance.hp;
        hptext.text = GameSystem.instance.hp.ToString() + "%(" + GameSystem.instance.hp.ToString() + "/" + GameSystem.instance.max_hp.ToString() + ")";
    }

    // Update is called once per frame
    void Update()
    {
        hpbar.value = GameSystem.instance.hp;
        hptext.text = GameSystem.instance.hp.ToString() + "%(" + GameSystem.instance.hp.ToString() + "/" + GameSystem.instance.max_hp.ToString() + ")";
    }
}

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
        hpbar.maxValue = PlayerState.instance.max_hp;
        hpbar.value = PlayerState.instance.hp;
        hptext.text = PlayerState.instance.hp.ToString() + "%(" + PlayerState.instance.hp.ToString() + "/" + PlayerState.instance.max_hp.ToString() + ")";
    }

    // Update is called once per frame
    void Update()
    {
        hpbar.value = PlayerState.instance.hp;
        hptext.text = PlayerState.instance.hp.ToString() + "%(" + PlayerState.instance.hp.ToString() + "/" + PlayerState.instance.max_hp.ToString() + ")";
    }
}

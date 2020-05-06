using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpBar : MonoBehaviour
{
    public Slider hpbar;
    public Text hptext;
    
    void Start()
    {
        hpbar.maxValue = PlayerState.instance.max_hp;
        hpbar.value = PlayerState.instance.hp;
        hptext.text = PlayerState.instance.hp.ToString() + "%(" + PlayerState.instance.hp.ToString() + "/" + PlayerState.instance.max_hp.ToString() + ")";
    }

    void Update()
    {
        hpbar.value = PlayerState.instance.hp;
        hptext.text = PlayerState.instance.hp.ToString() + "%(" + PlayerState.instance.hp.ToString() + "/" + PlayerState.instance.max_hp.ToString() + ")";
    }
}

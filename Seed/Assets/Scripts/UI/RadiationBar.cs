using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RadiationBar : MonoBehaviour
{
    public Slider radiationbar;
    public Text radiationtext;

    void Awake()
    {
        radiationbar.maxValue = GameSystem.instance.max_radiation;
        radiationbar.value = GameSystem.instance.radiation;
        radiationtext.text = GameSystem.instance.radiation.ToString() + "%(" + GameSystem.instance.radiation.ToString() + "/" + GameSystem.instance.max_radiation.ToString() + ")";
        StartCoroutine(Update_Radiation());
    }

    IEnumerator Update_Radiation()
    {
        while (true)
        {
            switch(GameSystem.instance.radiation_level)
            {
                case 1:
                    GameSystem.instance.radiation -= 0.01f;
                    GameSystem.instance.radiation = Mathf.Round(GameSystem.instance.radiation * 100) * 0.01f;
                    break;

                case 2:
                    GameSystem.instance.radiation -= 0.05f;
                    GameSystem.instance.radiation = Mathf.Round(GameSystem.instance.radiation * 100) * 0.01f;
                    break;

                case 3:
                    GameSystem.instance.radiation -= 0.1f;
                    GameSystem.instance.radiation = Mathf.Round(GameSystem.instance.radiation * 100) * 0.01f;
                    break;
            }

            radiationbar.value = GameSystem.instance.radiation;
            radiationtext.text = GameSystem.instance.radiation.ToString() + "%(" + GameSystem.instance.radiation.ToString() + "/" + GameSystem.instance.max_radiation.ToString() + ")";

            yield return new WaitForSeconds(1f);
        }
    }
}

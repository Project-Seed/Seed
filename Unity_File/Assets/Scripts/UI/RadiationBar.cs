using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RadiationBar : MonoBehaviour
{
    public Slider radiationbar;

    void Start()
    {
        radiationbar.maxValue = PlayerState.instance.max_radiation;
        radiationbar.value = PlayerState.instance.radiation;
        StartCoroutine(Update_Radiation());
    }

    IEnumerator Update_Radiation()
    {
        while (true)
        {
            switch(PlayerState.instance.radiation_level)
            {
                case 1:
                    PlayerState.instance.radiation += 1f;
                    PlayerState.instance.radiation = Mathf.Round(PlayerState.instance.radiation * 100) * 0.01f;
                    break;

                case 2:
                    PlayerState.instance.radiation += 5f;
                    PlayerState.instance.radiation = Mathf.Round(PlayerState.instance.radiation * 100) * 0.01f;
                    break;

                case 3:
                    PlayerState.instance.radiation += 10f;                  
                    PlayerState.instance.radiation = Mathf.Round(PlayerState.instance.radiation * 100) * 0.01f;
                    break;
            }

            radiationbar.value = PlayerState.instance.radiation;

            yield return new WaitForSeconds(1f);
        }
    }
}

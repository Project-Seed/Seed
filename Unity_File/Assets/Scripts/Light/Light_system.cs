using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteAlways]
public class Light_system : MonoBehaviour
{
    public static Light_system instance; // 현재 클레스를 인스턴트화

    [SerializeField] private Light DirectionalLight;
    [SerializeField] private Light_preset Preset;
    [SerializeField, Range(0, 24)] public float time;

    public Text text;
    public bool tuto = false;

    public static Light_system Instance
    {
        get { return instance; }
    }

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        if (Preset == null)
            return;

        if(Application.isPlaying && InputManager.instance.click_mod == 0 && tuto == false)
        {
            //time += Time.deltaTime / 150;
            time += Time.deltaTime / 2;
            time %= 24;
            updatelighting(time / 24f);
        }

        string itime = null;
        if ((int)time <= 12)
            itime = "AM " + (int)time;
        else
            itime = "PM " + (int)time;

        int mtime = (int)(time % 1 * 60);

        if(mtime < 10)
            text.text = itime + ":0" + mtime;
        else
            text.text = itime + ":" + mtime;

        if (time <= 5.5 || time >= 18.5)
            DirectionalLight.intensity = 0.1f;
        else if (time >= 6.5 && time <= 17.5)
            DirectionalLight.intensity = 1f;
        else if (time > 5.5 && time < 6.5)
            DirectionalLight.intensity = (time - 5.5f) / 0.9f + 0.1f;
        else
            DirectionalLight.intensity = 1f -((time - 17.5f) / 0.9f);
    }

    private void updatelighting(float timeprecent)
    {
        RenderSettings.ambientLight = Preset.AmbientColor.Evaluate(timeprecent);
        RenderSettings.fogColor = Preset.FogColor.Evaluate(timeprecent);

        if(DirectionalLight != null)
        {
            DirectionalLight.color = Preset.DirectionalColor.Evaluate(timeprecent);
            DirectionalLight.transform.localRotation = Quaternion.Euler(new Vector3((timeprecent * 360f) - 90f, 170f, 0));
        }
    }

    private void OnValidate()
    {
        if (DirectionalLight != null)
            return;

        if (RenderSettings.sun != null)
            DirectionalLight = RenderSettings.sun;
        else
        {
            Light[] lights = GameObject.FindObjectsOfType<Light>();
            foreach(Light light in lights)
            {
                if(light.type == LightType.Directional)
                {
                    DirectionalLight = light;
                    return;
                }
            }
        }
    }
}

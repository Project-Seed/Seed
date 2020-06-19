using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class Light_system : MonoBehaviour
{
    [SerializeField] private Light DirectionalLight;
    [SerializeField] private Light_preset Preset;
    [SerializeField, Range(0, 24)] private float time;

    private void Update()
    {
        if (Preset == null)
            return;

        if(Application.isPlaying)
        {
            time += Time.deltaTime;
            time %= 24;
            updatelighting(time / 24f);
        }
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

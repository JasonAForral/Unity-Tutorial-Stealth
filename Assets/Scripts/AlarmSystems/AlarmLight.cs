using UnityEngine;
using System.Collections;

public class AlarmLight : MonoBehaviour {

    public float fadeSpeed = 2f;
    public float highIntensity = 2f;
    public float lowIntensity = 0.5f;
    public float changeMargin = 0.2f;
    public bool alarmOn;

    private float targetIntensity;

    private Light lightComponent;

    void Awake ()
    {
        lightComponent = GetComponent<Light>();
        lightComponent.intensity = 0f;
        targetIntensity = highIntensity;
    }

    void Update ()
    {
        if (alarmOn)
        {
            lightComponent.intensity = Mathf.Lerp(lightComponent.intensity, targetIntensity, fadeSpeed * Time.deltaTime);
            CheckTargetIntensity();
        }
        else
        {
            lightComponent.intensity = Mathf.Lerp(lightComponent.intensity, 0f, fadeSpeed * Time.deltaTime);
        }
    }

    void CheckTargetIntensity ()
    {
        if (Mathf.Abs(targetIntensity - lightComponent.intensity) < changeMargin)
        {
            targetIntensity = (targetIntensity == highIntensity) ? lowIntensity : highIntensity;
        }
    }
}

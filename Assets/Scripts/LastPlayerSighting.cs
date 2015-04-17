using UnityEngine;
using System.Collections;

public class LastPlayerSighting : MonoBehaviour 
{
    public Vector3 position = Vector3.one * 1000f;
    public Vector3 resetPosition = Vector3.one * 1000;

    public float lightHighIntensity = 0.25f;
    public float lightLowIntensity = 0f;

    public float fadespeed = 7f;
    public float musicFadeSpeed = 1f;

    private AlarmLight alarm;
    private Light mainLight;
    private AudioSource panicAudio;
    private AudioSource[] sirens;

    AudioSource audio;

    void Awake ()
    {
        alarm = GameObject.FindGameObjectWithTag(Tags.alarm).GetComponent<AlarmLight>();
        mainLight = GameObject.FindGameObjectWithTag(Tags.alarm).GetComponent<Light>();
        panicAudio = transform.Find("secondaryMusic").GetComponent<AudioSource>();
        GameObject[] sirenGameObjects = GameObject.FindGameObjectsWithTag(Tags.siren);
        sirens = new AudioSource[sirenGameObjects.Length];
        for (int i = 0; i < sirens.Length; i++)
        {
            sirens[i] = sirenGameObjects[i].GetComponent<AudioSource>();
        }

        audio = GetComponent<AudioSource>();
    }

    void Update ()
    {
        SwitchAlarms();
        MusicFading();
    }

    void SwitchAlarms ()
    {
        Debug.Log("this");
        alarm.alarmOn = (position != resetPosition);

        float newIntensity;

        newIntensity = (position != resetPosition) ? lightLowIntensity : lightHighIntensity;

        mainLight.intensity = Mathf.Lerp(mainLight.intensity, newIntensity, fadespeed * Time.deltaTime);

        for (int i = 0; i < sirens.Length; i++)
        {
            if ((position != resetPosition) && !sirens[i].isPlaying)
            {
                sirens[i].Play();
            }
            else if (position == resetPosition)
            {
                sirens[i].Stop();
            }
        }

    }

    void MusicFading ()
    {
        if (position != resetPosition)
        {
            audio.volume = Mathf.Lerp(audio.volume, 0f, musicFadeSpeed * Time.deltaTime);
            panicAudio.volume = Mathf.Lerp(panicAudio.volume, 0.8f, musicFadeSpeed * Time.deltaTime);

        }
        else
        {
            audio.volume = Mathf.Lerp(audio.volume, 0.8f, musicFadeSpeed * Time.deltaTime);
            panicAudio.volume = Mathf.Lerp(panicAudio.volume, 0f, musicFadeSpeed * Time.deltaTime);
        }
    }
}

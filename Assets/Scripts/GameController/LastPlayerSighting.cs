using UnityEngine;
using System.Collections;

public class LastPlayerSighting : MonoBehaviour 
{
    public static Vector3 position = Vector3.one * 1000f;
    public static Vector3 resetPosition = Vector3.one * 1000f;

    public static float lightHighIntensity = 0.25f;
    public static float lightLowIntensity = 0f;

    public static float fadespeed = 7f;
    public static float musicFadeSpeed = 1f;

    public Light mainLight;
    public AlarmLight alarm;
    public AudioSource panicAudio;
    
    private AudioSource[] sirens;
    private AudioSource audioSource;

    void Awake ()
    {
        //alarm = GameObject.FindGameObjectWithTag(Tags.alarm).GetComponent<AlarmLight>();
        //mainLight = GameObject.FindGameObjectWithTag(Tags.alarm).GetComponent<Light>();
        //panicAudio = transform.Find("secondaryMusic").GetComponent<AudioSource>();
        audioSource = GetComponent<AudioSource>();
        
        GameObject[] sirenGameObjects = GameObject.FindGameObjectsWithTag(Tags.siren);
        sirens = new AudioSource[sirenGameObjects.Length];
        for (int i = 0; i < sirens.Length; i++)
        {
            sirens[i] = sirenGameObjects[i].GetComponent<AudioSource>();
        }
    }

    void Update ()
    {
        SwitchAlarms();
        MusicFading();
    }

    void SwitchAlarms ()
    {
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
            audioSource.volume = Mathf.Lerp(audioSource.volume, 0f, musicFadeSpeed * Time.deltaTime);
            panicAudio.volume = Mathf.Lerp(panicAudio.volume, 0.8f, musicFadeSpeed * Time.deltaTime);

        }
        else
        {
            audioSource.volume = Mathf.Lerp(audioSource.volume, 0.8f, musicFadeSpeed * Time.deltaTime);
            panicAudio.volume = Mathf.Lerp(panicAudio.volume, 0f, musicFadeSpeed * Time.deltaTime);
        }
    }
}

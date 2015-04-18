using UnityEngine;
using System.Collections;

public class LaserBlinking : MonoBehaviour {

    public float onTime;
    public float offTime;

    private MeshRenderer renderer;
    private Light lightSource;
    //private AudioSource audioSource;
    private float timer;

    void Awake ()
    {
        renderer = GetComponent<MeshRenderer>();
        lightSource = GetComponent<Light>();
        //audioSource = GetComponent<AudioSource>();
    }

    void Update () 
    {
        timer += Time.deltaTime;

        if (renderer.enabled && timer >= onTime)
        {
            SwitchBeam();
        }
        else if (!renderer.enabled && timer >= offTime)
        {
            SwitchBeam();
        }
    }

    void SwitchBeam ()
    {
        timer = 0f;

        renderer.enabled = !renderer.enabled;
        lightSource.enabled = !lightSource.enabled;
        //audioSource.enabled = !audioSource.enabled;
    }

}

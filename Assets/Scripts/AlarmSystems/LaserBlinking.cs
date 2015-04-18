using UnityEngine;
using System.Collections;

public class LaserBlinking : MonoBehaviour {

    public float onTime;
    public float offTime;

    private MeshRenderer meshRenderer;
    private Light lightSource;
    //private AudioSource audioSource;
    private float timer;

    void Awake ()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        lightSource = GetComponent<Light>();
        //audioSource = GetComponent<AudioSource>();
    }

    void Update () 
    {
        timer += Time.deltaTime;

        if (meshRenderer.enabled && timer >= onTime)
        {
            SwitchBeam();
        }
        else if (!meshRenderer.enabled && timer >= offTime)
        {
            SwitchBeam();
        }
    }

    void SwitchBeam ()
    {
        timer = 0f;

        meshRenderer.enabled = !meshRenderer.enabled;
        lightSource.enabled = !lightSource.enabled;
        //audioSource.enabled = !audioSource.enabled;
    }

}

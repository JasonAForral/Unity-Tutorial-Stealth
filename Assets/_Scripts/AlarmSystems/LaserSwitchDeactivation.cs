using UnityEngine;
using System.Collections;

public class LaserSwitchDeactivation : MonoBehaviour
{
    public GameObject laser;
    public Material unlockedMat;
    public MeshRenderer screen;

    private AudioSource audioSource;

    void Awake ()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void OnTriggerStay (Collider other)
    {
        if(other.gameObject.CompareTag(Tags.player))
        {
            if (Input.GetButton("Interact"))
            {
                LaserDeactivation();
            }
        }
    }

    void LaserDeactivation ()
    {
        laser.SetActive(false);

        screen.material = unlockedMat;
        audioSource.Play();
        
    }
}

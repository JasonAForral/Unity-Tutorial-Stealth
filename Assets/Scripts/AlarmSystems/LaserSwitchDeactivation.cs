using UnityEngine;
using System.Collections;

public class LaserSwitchDeactivation : MonoBehaviour
{
    public GameObject laser;
    public Material unlockedMat;
    public MeshRenderer screen;

    public Transform player;

    private AudioSource audioSource;

    void Awake ()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void OnTriggerStay (Collider other)
    {
        if(other.gameObject.CompareTag(Tags.player))
        {
            if (Input.GetButton("Switch"))
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

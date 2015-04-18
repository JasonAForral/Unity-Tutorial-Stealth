using UnityEngine;
using System.Collections;

public class DoorAnimation : MonoBehaviour {

    public HashIDs hash;
    //public GameObject player;
    public PlayerInventory playerInventory;
    
    
    public bool requireKey;
    public AudioClip doorSwishClip;
    public AudioClip accessDeniedClip;

    private Animator anim;
    //private HashIDs hash;
    //private GameObject player;
    //private PlayerInventory playerInventory;
    private int count;

    private AudioSource audioSource;

    void Awake ()
    {
        anim = GetComponent<Animator>();
        //hash = GameObject.FindGameObjectWithTag(Tags.gameController).GetComponent<HashIDs>();
        //player = GameObject.FindGameObjectWithTag(Tags.player);
        //playerInventory = player.GetComponent<PlayerInventory>();

        audioSource = GetComponent<AudioSource>();
        Debug.Log("Awake");
    }

    void OnTriggerEnter (Collider other)
    {
        Debug.Log("Enter");
        if (other.CompareTag(Tags.player))
        {
            if (requireKey)
            {
                if (playerInventory.hasKey)
                {
                    count++;
                }
                else
                {
                    audioSource.clip = accessDeniedClip;
                    audioSource.Play();
                }
            }
            else
            {
                count++;
            }
        } 
        else if(other.CompareTag(Tags.enemy))
        {
            if (other is CapsuleCollider)
            {
                count++;
            }
        }
    }

    void OnTriggerExit (Collider other)
    {
        if (other.CompareTag(Tags.player) || (other.CompareTag(Tags.enemy) && other is CapsuleCollider))
        {
            count = Mathf.Max(0, count - 1);
        }
    }

    void Update ()
    {
        anim.SetBool(hash.openBool, count > 0);

        if (anim.IsInTransition(0) && !audioSource.isPlaying)
        {
            audioSource.clip = doorSwishClip;
            audioSource.Play();
        }
    }
}
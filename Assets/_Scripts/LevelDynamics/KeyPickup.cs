using UnityEngine;
using System.Collections;

public class KeyPickup : MonoBehaviour
{
    public AudioClip keyGrab;
    public GameObject player;
    private PlayerInventory playerInventory;

    void Awake ()
    {
        playerInventory = player.GetComponent<PlayerInventory>();
    }

    void OnTriggerEnter (Collider other)
    {
        if (other.gameObject == player)
        {
            AudioSource.PlayClipAtPoint(keyGrab, transform.position);
            playerInventory.hasKey = true;
            Destroy(gameObject);
        }
    }
}


using UnityEngine;
using System.Collections;

public class CCTVPlayerDetection : MonoBehaviour {

    void Awake ()
    {
    }

    void OnTriggerStay (Collider other)
    {
        if (other.gameObject.CompareTag(Tags.player))
        {
            Vector3 relPlayerPos = PlayerMovement.Position - transform.position;
            RaycastHit hit;

            if (Physics.Raycast(transform.position, relPlayerPos, out hit))
            {
                if(hit.collider.gameObject.CompareTag(Tags.player))
                {
                    LastPlayerSighting.position = PlayerMovement.Position;
                }
            }
        }
    }
}

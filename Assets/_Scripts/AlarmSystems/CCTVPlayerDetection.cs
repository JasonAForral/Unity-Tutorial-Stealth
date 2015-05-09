using UnityEngine;
using System.Collections;

public class CCTVPlayerDetection : MonoBehaviour {

    public Transform player;
    public LastPlayerSighting lastPlayerSighting;

    void Awake ()
    {
        //player = GameObject.FindGameObjectWithTag(Tags.player);
        //lastPlayerSighting = GameObject.FindGameObjectWithTag(Tags.gameController).GetComponent<LastPlayerSighting>();
    }

    void OnTriggerStay (Collider other)
    {
        if (other.gameObject.CompareTag(Tags.player))
        {
            Vector3 relPlayerPos = player.position - transform.position;
            RaycastHit hit;

            if (Physics.Raycast(transform.position, relPlayerPos, out hit))
            {
                if(hit.collider.gameObject.CompareTag(Tags.player))
                {
                    LastPlayerSighting.position = player.position;
                }
            }
        }
    }
}

using UnityEngine;
using System.Collections;

public class LaserPlayerDetection : MonoBehaviour {

    public Transform player;
    public LastPlayerSighting lastPlayerSighting;
    
    private MeshRenderer renderer;
    
    void Awake ()
    {
        //player = GameObject.FindGameObjectWithTag(Tags.player);
        //lastPlayerSighting = GameObject.FindGameObjectWithTag(Tags.gameController).GetComponent<LastPlayerSighting>();
        renderer = GetComponent<MeshRenderer>();

    }

    void OnTriggerStay (Collider other)
    {
        if (renderer.enabled)
        {
            if (other.gameObject.CompareTag(Tags.player))
            {
                lastPlayerSighting.position = player.position;
            }
        }
    }
}

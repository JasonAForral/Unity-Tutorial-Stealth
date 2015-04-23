using UnityEngine;
using System.Collections;

public class LaserPlayerDetection : MonoBehaviour {

    public Transform player;
    public LastPlayerSighting lastPlayerSighting;
    
    private MeshRenderer meshRenderer;
    
    void Awake ()
    {
        //player = GameObject.FindGameObjectWithTag(Tags.player);
        //lastPlayerSighting = GameObject.FindGameObjectWithTag(Tags.gameController).GetComponent<LastPlayerSighting>();
        meshRenderer = GetComponent<MeshRenderer>();

    }

    void OnTriggerStay (Collider other)
    {
        if (meshRenderer.enabled)
        {
            if (other.gameObject.CompareTag(Tags.player))
            {
                LastPlayerSighting.position = player.position;
            }
        }
    }
}

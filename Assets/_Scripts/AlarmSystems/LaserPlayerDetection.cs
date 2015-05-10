using UnityEngine;
using System.Collections;

public class LaserPlayerDetection : MonoBehaviour {

    private MeshRenderer meshRenderer;
    
    void Awake ()
    {
        meshRenderer = GetComponent<MeshRenderer>();

    }

    void OnTriggerStay (Collider other)
    {
        if (meshRenderer.enabled)
        {
            if (other.gameObject.CompareTag(Tags.player))
            {
                LastPlayerSighting.position = PlayerMovement.Position;
            }
        }
    }
}

using UnityEngine;
using System.Collections;

public class MiniMapController : MonoBehaviour {

    public Transform focus;

    private Vector3 offset;

    

	// Use this for initialization
	void Awake () {
        offset = transform.position - focus.position;
	}
	
	// Update is called once per frame
	void LateUpdate () {
        //transform.position = focus.position + offset;

        transform.position = Vector3.Lerp(transform.position, focus.position + offset + focus.forward * 2f, 0.1f);
	}
}

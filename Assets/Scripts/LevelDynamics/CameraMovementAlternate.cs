using UnityEngine;
using System.Collections;

public class CameraMovementAlternate : MonoBehaviour {

    public Transform player;
    public Transform cameraTilt;
    public Transform cameraZoom;

    private Vector3 cameraOffset;

    //private float maxZoom;

    void Awake ()
    {
        cameraOffset = transform.position - player.position;
    }

    void Update ()
    {
        transform.position = player.position + cameraOffset;
        if (Input.GetMouseButton(1))
        {
            transform.Rotate(Vector3.up * Input.GetAxis("Mouse X"));
            cameraTilt.Rotate(Vector3.left * Input.GetAxis("Mouse Y"));
            cameraTilt.localRotation = Quaternion.Euler(Vector3.right * Mathf.Clamp(cameraTilt.eulerAngles.x, 5f, 80f));
        }

        cameraZoom.Translate(Vector3.forward * 50 * Input.GetAxis("Mouse Scroll"), cameraZoom);
        cameraZoom.localPosition = Vector3.forward * Mathf.Clamp(cameraZoom.localPosition.z, -10, -1);
        /*
        if raycast behind 
         * 
         */

    }

    void LateUpdate ()
    {
        
    } 
}

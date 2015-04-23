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
            float verticalRotation = cameraTilt.eulerAngles.x;
            if (verticalRotation > 180) { verticalRotation -= 360f; }
            cameraTilt.localEulerAngles = Vector3.right * Mathf.Clamp(verticalRotation, -80f, 80f);
        }

        float mouseScroll = Input.GetAxis("Mouse Scroll");
        cameraZoom.Translate(Vector3.forward * 50 * mouseScroll, cameraZoom);
        cameraZoom.localPosition = Vector3.forward *  Mathf.Clamp(cameraZoom.localPosition.z, -10f, 0f);
        
        /*
         * do stuff to limit zoom here
        if raycast behind 
         * 
         */

    }

    void LateUpdate ()
    {
        
    } 
}

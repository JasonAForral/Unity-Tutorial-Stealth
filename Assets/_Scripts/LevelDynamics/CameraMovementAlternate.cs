using UnityEngine;
using System.Collections;

public class CameraMovementAlternate : MonoBehaviour {

    public Transform player;
    public Transform cameraTilt;
    public Transform cameraZoom;

    public Vector3 cameraOffset;

    [SerializeField]
    private float zoomSpeed = 5f;
    

    private float zoomTarget;
    private float zoomCurent;

    void Awake ()
    {
        cameraOffset = transform.position - player.position;

        if (null == cameraTilt)
            cameraTilt = transform.GetChild(0);
        if (null == cameraZoom)
            cameraZoom = cameraTilt.GetChild(0);

        zoomTarget = zoomCurent = cameraZoom.localPosition.z;
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

        zoomCurent = cameraZoom.localPosition.z;
        zoomTarget += 50f * Input.GetAxis("Mouse Scroll");
        zoomTarget = Mathf.Clamp(zoomTarget, -5f, -1f);

        if (Mathf.Abs(zoomTarget - zoomCurent) > 0.05f)
            cameraZoom.localPosition = Vector3.forward * Mathf.Lerp(zoomCurent, zoomTarget, zoomSpeed * Time.deltaTime);

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

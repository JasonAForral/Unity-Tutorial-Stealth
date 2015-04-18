using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour {

    public float smooth = 1.5f;
    
    public Transform player;
    private Vector3 relCameraPos;

    private float relCameraPosMag;
    private Vector3 newPos;

	// Use this for initialization
	void Awake () {

        // find game object with tag bla bla here
        relCameraPos = transform.position - player.position;
        relCameraPosMag = relCameraPos.magnitude - 0.5f;
        
	}

    void Update ()
    {
        if (Input.GetMouseButton(1))
        {
            transform.Translate(Vector3.right * Input.GetAxis("Mouse X") + Vector3.up * Input.GetAxis("Mouse Y"));
            transform.Rotate(Vector3.up * Input.GetAxis("Mouse X") );
        }
    }
	
	void FixedUpdate () {
        Vector3 standardPos = player.position + relCameraPos;
        Vector3 abovePos = player.position + Vector3.up * relCameraPosMag;
        Vector3[] checkPoints = new Vector3[5];
        checkPoints[0] = standardPos;
        checkPoints[1] = Vector3.Lerp(standardPos, abovePos, 0.25f);
        checkPoints[2] = Vector3.Lerp(standardPos, abovePos, 0.5f);
        checkPoints[3] = Vector3.Lerp(standardPos, abovePos, 0.75f);
        checkPoints[4] = abovePos;

        for (int i = 0; i < checkPoints.Length; i++)
        {
            if(ViewingPosCheck(checkPoints[i]))
            {
                break;
            }
        }

        transform.position = Vector3.Lerp(transform.position, newPos, smooth * Time.deltaTime);
        SmoothLookAt();
	}

    void LateUpdate ()
    {
        //transform.position = Vector3.Lerp(transform.position, player.position + relCameraPos, 0.001);
    }


    bool ViewingPosCheck (Vector3 checkPos)
    {
        RaycastHit hit;
        if (Physics.Raycast(checkPos, player.position - checkPos, out hit, relCameraPosMag))
        {
            if (hit.transform != player) // (hit.collider.CompareTag(Tags.player))
            {
                return false;
            }
        }

        newPos = checkPos;
        return true;
    }

    void SmoothLookAt ()
    {
        Vector3 relPlayerPosition = player.position - transform.position;
        Quaternion LookAtRotation = Quaternion.LookRotation(relPlayerPosition, Vector3.up);
        transform.rotation = Quaternion.Lerp(transform.rotation, LookAtRotation, smooth * Time.deltaTime);
    }
}

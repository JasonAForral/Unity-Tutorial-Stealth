using UnityEngine;
using System.Collections;

public class LiftDoorsTracking : MonoBehaviour {

    public float doorSpeed = 7f;

    public Transform leftOuterDoor;
    public Transform rightOuterDoor;
    public Transform leftInnerDoor;
    public Transform rightInnerDoor;
    private float leftClosedPosX;
    private float rightClosedPosX;

    void Awake ()
    {
        //leftOuterDoor = gameObject.;
        //rightOuterDoor;
        //leftInnerDoor;
        //RightInnerDoor

        leftClosedPosX = leftInnerDoor.position.x;
        rightClosedPosX = rightInnerDoor.position.x;
    }

    void MoveDoors (float newLeftXTarget, float newRightXTarget)
    {
        Vector3 doorVector3 = leftInnerDoor.position;
        float newX = Mathf.Lerp(doorVector3.x, newLeftXTarget, doorSpeed * Time.deltaTime);
        leftInnerDoor.position = new Vector3(newX, doorVector3.y, doorVector3.z);

        doorVector3 = rightInnerDoor.position;
        newX = Mathf.Lerp(doorVector3.x, newLeftXTarget, doorSpeed * Time.deltaTime);
        rightInnerDoor.position = new Vector3(newX, doorVector3.y, doorVector3.z);
    }

    public void DoorFollowing ()
    {
        MoveDoors(leftOuterDoor.position.x, rightOuterDoor.position.x);
    }

    public void CloseDoors ()
    {
        MoveDoors(leftClosedPosX, rightClosedPosX);
    }

}

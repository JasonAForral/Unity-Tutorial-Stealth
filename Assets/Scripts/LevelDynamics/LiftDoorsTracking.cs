using UnityEngine;
using System.Collections;

public class LiftDoorsTracking : MonoBehaviour {

    public float doorSpeed = 7f;

    public Transform leftOuterDoor;
    public Transform rightOuterDoor;
    public Transform leftInnerDoor;
    public Transform rightInnerDoor;
    public float leftClosedPosX;
    public float rightClosedPosX;

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

        //float newX = Mathf.Lerp(leftInnerDoor.position.x, newLeftXTarget, doorSpeed * Time.deltaTime);
        //leftInnerDoor.position = new Vector3(newX, leftInnerDoor.position.y, leftInnerDoor.position.z);

        Vector3 InitialPosition = leftInnerDoor.position;
        float newX = Mathf.Lerp(InitialPosition.x, newLeftXTarget, doorSpeed * Time.deltaTime);
        leftInnerDoor.position = new Vector3(newX, InitialPosition.y, InitialPosition.z);

        InitialPosition = rightInnerDoor.position;
        newX = Mathf.Lerp(InitialPosition.x, newRightXTarget, doorSpeed * Time.deltaTime);
        rightInnerDoor.position = new Vector3(newX, InitialPosition.y, InitialPosition.z);
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

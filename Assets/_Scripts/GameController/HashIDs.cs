using UnityEngine;
using System.Collections;

public class HashIDs : MonoBehaviour {

    public static int dyingState;
    public static int deadBool;
    public static int locomotionSate;
    public static int shoutSate;
    public static int speedFloat;
    public static int sneakingBool;
    public static int shoutingBool;
    public static int playerInSightBool;
    public static int shotFloat;
    public static int aimWeightFloat;
    public static int angularSpeedFloat;
    public static int openBool;

    void Awake ()
    {
        dyingState = Animator.StringToHash("Base Layer.Dying");
        deadBool = Animator.StringToHash("Dead");
        locomotionSate = Animator.StringToHash("Base Layer.Locomotion");
        shoutSate = Animator.StringToHash("Shouting.Shout");
        speedFloat = Animator.StringToHash("Speed");
        sneakingBool = Animator.StringToHash("Sneaking");
        shoutingBool = Animator.StringToHash("Shouting");
        playerInSightBool = Animator.StringToHash("PlayerInSight");
        shotFloat = Animator.StringToHash("Shot");
        aimWeightFloat = Animator.StringToHash("AimWeight");
        angularSpeedFloat = Animator.StringToHash("AngularSpeed");
        openBool = Animator.StringToHash("Open");

    }
}

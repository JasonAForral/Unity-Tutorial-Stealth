﻿using UnityEngine;
using System.Collections;

public class AnimatorSetup
{
    public float speedDampTime = 0.1f;
    public float angularSpeedDampTime = 0.7f;
    public float angleResponseTime = 0.6f;
    public float angleResponseTimeInverse = 1.66f;
    
    private Animator anim;
    //private HashIDs hash;

    public AnimatorSetup( Animator animator)
    {
        anim = animator;
        //hash = hashIDs;
    }

    public void Setup(float speed, float angle)
    {
        float angularSpeed = angle * angleResponseTimeInverse;// / angleResponseTime;

        anim.SetFloat(HashIDs.speedFloat, speed, speedDampTime, Time.deltaTime);
        anim.SetFloat(HashIDs.angularSpeedFloat, angularSpeed, angularSpeedDampTime, Time.deltaTime);
    }
}

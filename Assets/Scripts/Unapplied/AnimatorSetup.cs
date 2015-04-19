﻿using UnityEngine;
using System.Collections;

public class AnimatorSetup
{
    public float speedDampTime = 0.1f;
    public float angularSpeedDampTime = 0.7f;
    public float angleResponseTimeInverse = 1f/0.6f ;

    private Animator anim;
    private HashIDs hash;

    public AnimatorSetup( Animator animator, HashIDs hashIDs)
    {
        anim = animator;
        hash = hashIDs;
    }

    public void Setup(float speed, float angle)
    {
        float angualrSpeed = angle * angleResponseTimeInverse;

        anim.SetFloat(hash.angularSpeedFloat, speed, speedDampTime, Time.deltaTime);
        anim.SetFloat(hash.angularSpeedFloat, angualrSpeed, angularSpeedDampTime, Time.deltaTime);
    }
}

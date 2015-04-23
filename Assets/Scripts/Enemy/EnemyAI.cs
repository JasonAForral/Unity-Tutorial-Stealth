using UnityEngine;
using System.Collections;

public class EnemyAI : MonoBehaviour {
    public float patrolSpeed = 2f;
    public float chaseSpeed = 5f;
    public float chaseWaitTime =  5f;
    public float patrolWaitTime =  1f;
    public Transform[] patrolWayPoints;

    public PlayerHealth playerHealth;
    //public LastPlayerSighting lastPlayerSighting;

    private EnemySight enemySight;
    private NavMeshAgent nav;

    private float chaseTimer;
    private float patrolTimer;
    private int wayPointIndex;

    void Awake()
    {
        playerHealth = GameObject.FindGameObjectWithTag(Tags.player).GetComponent<PlayerHealth>();
        //lastPlayerSighting = GameObject.FindGameObjectWithTag(Tags.gameController).GetComponent<LastPlayerSighting>();

        enemySight = GetComponent<EnemySight>();
        nav = GetComponent<NavMeshAgent>();
    }

    void Update ()
    {
        if (enemySight.playerInSight && 0f < playerHealth.health)
            Shooting();
        else if (enemySight.personalLastSighting != LastPlayerSighting.resetPosition && 0f < playerHealth.health)
            Chasing();
        else
            Patrolling();
    }

    void Shooting ()
    {
        nav.Stop();
    }

    void Chasing ()
    {
        Vector3 sightingDeltaPos = enemySight.personalLastSighting - transform.position;
        if (4f < sightingDeltaPos.sqrMagnitude)
            nav.destination = enemySight.personalLastSighting;

        nav.speed = chaseSpeed;

        if (nav.remainingDistance < nav.stoppingDistance)
        {
            chaseTimer += Time.deltaTime;
            
            if (chaseTimer >= chaseWaitTime)
            {
                LastPlayerSighting.position = LastPlayerSighting.resetPosition;
                enemySight.personalLastSighting = LastPlayerSighting.resetPosition;
                chaseTimer = 0f;
            }
        }
        else
            chaseTimer = 0f;
    }

    void Patrolling ()
    {
        nav.speed = patrolSpeed;
        if (nav.destination == LastPlayerSighting.resetPosition || nav.remainingDistance < nav.stoppingDistance)
        {
            patrolTimer += Time.deltaTime;

            if (patrolTimer >= patrolWaitTime)
            {
                if (wayPointIndex == patrolWayPoints.Length - 1)
                    wayPointIndex = 0;
                else
                    wayPointIndex++;

                // Reset the timer.
                patrolTimer = 0;
            }
        }
        else
        {
            patrolTimer = 0;
        }
        
        nav.destination = patrolWayPoints[wayPointIndex].position;
    }
}
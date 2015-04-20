using UnityEngine;
using System.Collections;

public class EnemyAI : MonoBehaviour {
    public float patrolSpeed = 2f;
    public float chaseSpeed = 5f;
    public float chaseWaitTime =  5f;
    public float patrolWaitTime =  1f;
    public Transform[] patrolWayPoints;

    public Transform player;
    public PlayerHealth playerHealth;
    public LastPlayerSighting lastPlayerSighting;

    private EnemySight enemySight;
    private NavMeshAgent nav;

    private float chaseTimer;
    private float patrolTimer;
    private int wayPointIndex;

    void Awake()
    {
        //player = GameObject.FindGameObjectWithTag(Tags.player).transform;
        //playerHealth = player.GetComponent<PlayerHealth>();
        //lastPlayerSighting = GameObject.FindGameObjectWithTag(Tags.gameController).GetComponent<LastPlayerSighting>();

        enemySight = GetComponent<EnemySight>();
        nav = GetComponent<NavMeshAgent>();
    }

    void Update ()
    {
        if (enemySight.playerInSight && 0f < playerHealth.health)
            Shooting();
        else if (enemySight.personalLastSighting != lastPlayerSighting.resetPosition && 0f < playerHealth.health)
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

            if (chaseTimer > chaseWaitTime)
            {
                lastPlayerSighting.position = lastPlayerSighting.resetPosition;
                enemySight.personalLastSighting = lastPlayerSighting.resetPosition;
                chaseTimer = 0f;
            }
        }
        else
            chaseTimer = 0f;
    }

    void Patrolling ()
    {
        nav.speed = patrolSpeed;

        if (nav.destination == lastPlayerSighting.resetPosition || nav.remainingDistance < nav.stoppingDistance)
        {
            patrolTimer += Time.deltaTime;

            if (patrolTimer >= patrolWaitTime)
            {
                if (patrolWayPoints.Length - 1 == wayPointIndex)
                    wayPointIndex = 0;
                else
                    wayPointIndex++;

                patrolTimer = 0f;
            }
        }
        else
            patrolTimer = 0f;

        nav.destination = patrolWayPoints[wayPointIndex].position;
    }
}
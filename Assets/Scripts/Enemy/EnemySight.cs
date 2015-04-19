using UnityEngine;
using System.Collections;

public class EnemySight : MonoBehaviour {

    public float fieldOfViewAngle = 110f;
    public bool playerInSight;
    public Vector3 personalLastSighting;

    private NavMeshAgent nav;
    private SphereCollider col;
    private Animator anim;

    private GameObject gameController;
    private LastPlayerSighting lastPlayerSighting;
    private HashIDs hash;
    
    private GameObject playerObj;
    private Animator playerAnim;
    private PlayerHealth playerHealth;
    
    private Vector3 previousSighting;

    void Awake ()
    {
        nav = GetComponent<NavMeshAgent>();
        col = GetComponent<SphereCollider>();
        anim = GetComponent<Animator>();

        gameController = GameObject.FindGameObjectWithTag(Tags.gameController);
        hash = gameController.GetComponent<HashIDs>();
        lastPlayerSighting = gameController.GetComponent<LastPlayerSighting>();

        playerObj = GameObject.FindGameObjectWithTag(Tags.player);//.GetComponent<Transform>();
        playerAnim = playerObj.GetComponent<Animator>();
        playerHealth = playerObj.GetComponent<PlayerHealth>();

        Vector3 resetPosition = lastPlayerSighting.resetPosition;
        personalLastSighting = resetPosition;
        previousSighting = resetPosition;
    }

    void Update ()
    {
        Vector3 globalPlayerSighting = lastPlayerSighting.position;
        
        if (globalPlayerSighting != previousSighting)
        {
            personalLastSighting = globalPlayerSighting;
        }
        previousSighting = globalPlayerSighting;

        if (0 < playerHealth.health)
            anim.SetBool(hash.playerInSightBool, playerInSight);
        else
            anim.SetBool(hash.playerInSightBool, false);
    }

    void OnTriggerStay (Collider other)
    {
        if (other.CompareTag(Tags.player))
        {
            playerInSight = false;

            
            Vector3 myPosition = transform.position;
            Vector3 playerPosition = playerObj.transform.position;

            Vector3 direction = other.transform.position - myPosition;

            float angle = Vector3.Angle(direction, transform.forward);

            if (angle < fieldOfViewAngle * 0.5f)
            {
                RaycastHit hit;

                if (Physics.Raycast(myPosition + transform.up, direction.normalized, out hit, col.radius))
                {
                    if (hit.collider.CompareTag(Tags.player))
                    {
                        playerInSight = true;
                        lastPlayerSighting.position = playerPosition;
                    }
                }
            }

            int playerLayerZeroStateHash = playerAnim.GetCurrentAnimatorStateInfo(0).fullPathHash;
            int playerLayerOneStateHash = playerAnim.GetCurrentAnimatorStateInfo(1).fullPathHash;

            if (hash.locomotionSate == playerLayerZeroStateHash || hash.shoutSate == playerLayerOneStateHash)
            {
                if (CalculatePathLength(playerPosition) <= col.radius)
                {
                    personalLastSighting = playerPosition;
                }
            }
        }
    }

    void OnTriggerExit (Collider other)
    {
        if(other.CompareTag(Tags.player))
        {
            playerInSight = false;
        }
    }

    float CalculatePathLength (Vector3 targetPosition)
    {
        NavMeshPath path = new NavMeshPath();

        if (nav.enabled)
        {
            nav.CalculatePath(targetPosition, path);
        }

        Vector3[] allWayPoints = new Vector3[path.corners.Length + 2];

        allWayPoints[0] = transform.position;
        allWayPoints[allWayPoints.Length - 1] = targetPosition;

        for (int i=0; i < path.corners.Length; i++)
        {
            allWayPoints[i + 1] = path.corners[i];
        }

        float pathLength = 0f;

        for (int i=0; i < allWayPoints.Length-1; i++)
        {
            pathLength += Vector3.Distance(allWayPoints[i], allWayPoints[i+1]);
        }

        return pathLength;
    }
}

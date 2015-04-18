using UnityEngine;
using System.Collections;

public class LiftTrigger : MonoBehaviour {

    public float timeToDoorsClose = 2f;
    public float timeToLiftStart = 3f;
    public float timeToEndLevel = 6f;
    public float liftSpeed = 3f;

    public GameObject player;
    //private GameObject player;
    private Animator playerAnim;
    public HashIDs hash;
    private CameraMovement camMovement;
    public SceneFadeInOut sceneFadeInOut;
    private LiftDoorsTracking liftDoorsTracking;
    private bool playerInLift;
    private float timer;

    private AudioSource audioSource;

    void Awake ()
    {
        //player = GameObject.FindGameObjectWithTag(Tags.player);
        //hash = GameObject.FindGameObjectWithTag(Tags.gameController).GetComponent<HashIDs>();
        //sceneFadeInOut = GameObject.FindGameObjectWithTag(Tags.fader).GetComponent<SceneFadeInOut>();
        
        playerAnim = player.GetComponent<Animator>();
        
        camMovement = Camera.main.GetComponent<CameraMovement>();
        liftDoorsTracking = GetComponent<LiftDoorsTracking>();

        audioSource = GetComponent<AudioSource>();
    }

    void OnTriggerEnter (Collider other)
    {
        if (other.CompareTag(Tags.player))
        {
            playerInLift = true;
        }

    }
    void OnTriggerExit (Collider other)
    {
        if (other.CompareTag(Tags.player))
        {
            playerInLift = false;
            timer = 0f;
        }
    }

    void Update ()
    {
        if (playerInLift)
            LiftActivation();

        if (timer < timeToDoorsClose)
        {
            liftDoorsTracking.DoorFollowing();
        }
        else 
        {
            liftDoorsTracking.CloseDoors();
        }
    }

    void LiftActivation ()
    {
        timer += Time.deltaTime;
        if (timer >= timeToLiftStart)
        {
            playerAnim.SetFloat(hash.speedFloat, 0f);
            camMovement.enabled = false;
            player.transform.parent = transform;

            transform.Translate(Vector3.up * liftSpeed * Time.deltaTime);

            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }

            if (timer >= timeToEndLevel)
                sceneFadeInOut.EndScene();
        }
    }

}

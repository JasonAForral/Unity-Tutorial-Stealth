using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

    public AudioClip shoutingClip;
    public float turnSmoothing = 15f;
    public float speedDampTime = 0.1f;

    private Animator anim;
    public HashIDs hash;

    private Rigidbody rigidBody;
    private AudioSource audioSource;

    public Transform cameraPan;
    //private HashIDs hash;

    void Awake ()
    {
        anim = GetComponent<Animator>();
        //hash = GameObject.FindGameObjectWithTag(Tags.gameController).GetComponent<HashIDs>();

        rigidBody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();

        anim.SetLayerWeight(1, 1f);


    }

    void FixedUpdate ()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        bool sneak = Input.GetButton("Sneak");

        MovementManagement(h, v, sneak);
    }

    void Update ()
    {
        bool shout = Input.GetButtonDown("Attract");
        anim.SetBool(hash.shoutingBool, shout);
        AudioManagement(shout);
    }

    void LateUpdate ()
    {
        cameraPan.position = transform.position;
        //cameraPan.LookAt(Camera.main.transform.position, Vector3.up);
        //cameraPan.rotation = Camera.main.transform.position, Vector3.up);
        cameraPan.localRotation = Quaternion.Euler(Vector3.up * Camera.main.transform.rotation.eulerAngles.y);
    }

    void MovementManagement (float horizontal, float vertical, bool sneaking)
    {
        anim.SetBool(hash.sneakingBool, sneaking);

        if (0 != horizontal || 0 != vertical)
        {
            Rotating(horizontal, vertical);
            anim.SetFloat(hash.speedFloat, 5.5f, speedDampTime, Time.deltaTime);
        }
        else
        {
            anim.SetFloat (hash.speedFloat, 0f);
        }
    }

    void Rotating (float horizontal, float vertical)
    {
        // Vector3 targetDirection = Vector3.right * horizontal + Vector3.forward * vertical;
        //Vector3 targetDirection = new Vector3(horizontal, 0.0f, vertical);

        //Vector3.

        // use camera based direction instead
        Vector3 targetDirection = cameraPan.TransformDirection(new Vector3(horizontal, 0.0f, vertical));
        

        Quaternion targetRotation = Quaternion.LookRotation(targetDirection, Vector3.up);
        Quaternion newRotation = Quaternion.Lerp(rigidBody.rotation, targetRotation, turnSmoothing * Time.deltaTime);
        rigidBody.MoveRotation(newRotation);
    }

    void AudioManagement (bool shout)
    {
        if (hash.locomotionSate == anim.GetCurrentAnimatorStateInfo(0).fullPathHash)
        {
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
        else
        {
            audioSource.Stop();
        }

        if (shout)
        {
            AudioSource.PlayClipAtPoint(shoutingClip, transform.position);
        }
    }
}

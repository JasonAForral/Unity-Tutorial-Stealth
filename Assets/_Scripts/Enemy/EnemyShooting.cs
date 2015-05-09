using UnityEngine;
using System.Collections;

public class EnemyShooting : MonoBehaviour {

    public LineRenderer laserShotLine;
    
    public static float maximumDamage = 120f;
    public static float minimumDamage = 45f;
    public AudioClip shotClip;
    public static float flashIntensity = 3f;
    public static float fadeSpeed = 10f;

    private Animator anim;
    //private HashIDs hash;
    
    private Light laserShotLight;
    private SphereCollider col;
    private Transform player;
    private PlayerHealth playerHealth;

    private bool shooting;
    private float scaledDamage;

    public float colRadiusRecip;

    void Awake ()
    {
        anim = GetComponent<Animator>();

        player = GameObject.FindGameObjectWithTag(Tags.player).transform;
        //laserShotLine = GetComponentInChildren<LineRenderer>();
        laserShotLight = laserShotLine.GetComponent<Light>();
        col = GetComponent<SphereCollider>();
        player = GameObject.FindGameObjectWithTag(Tags.player).transform;
        playerHealth = player.gameObject.GetComponent<PlayerHealth>();
        //hash = GameObject.FindGameObjectWithTag(Tags.gameController).GetComponent<HashIDs>();

        laserShotLine.enabled = false;
        laserShotLight.intensity = 0f;

        colRadiusRecip = 1f / col.radius;

        scaledDamage = maximumDamage - minimumDamage;
    }

    void Update ()
    {
        float shot = anim.GetFloat(HashIDs.shotFloat);
        if (shot > 0.5f && !shooting)
            // shots fired!
            Shoot();
        if (shot < 0.5f)
        {
            shooting = false;
            laserShotLine.enabled = false;
        }

        laserShotLight.intensity = Mathf.Lerp(laserShotLight.intensity, 0f, fadeSpeed * Time.deltaTime);
    }

    void OnAnimatorIK (int layerIndex)
    {
        float aimWeight = anim.GetFloat(HashIDs.aimWeightFloat);
        anim.SetIKPosition(AvatarIKGoal.RightHand, player.position + Vector3.up * 1f);
        anim.SetIKPositionWeight(AvatarIKGoal.RightHand, aimWeight);
    }

    void Shoot ()
    {
        shooting = true;
        float fractionalDistance = (col.radius - Vector3.Distance(transform.position, player.position)) * colRadiusRecip;
        float damage = scaledDamage * fractionalDistance + minimumDamage;
        playerHealth.TakeDamage(damage);
        ShotEffects();
    }

    void ShotEffects ()
    {
        Vector3 laserPosition = laserShotLine.transform.position;
        laserShotLine.SetPosition(0, laserPosition);
        laserShotLine.SetPosition(1, player.position + Vector3.up * 1.5f);
        laserShotLine.enabled = true;
        laserShotLight.intensity = flashIntensity;
        AudioSource.PlayClipAtPoint(shotClip, laserPosition);
    }
}



using UnityEngine;
using System.Collections;

public class EnemyShooting : MonoBehaviour {

    public float maximumDamage = 120f;
    public float minimumDamage = 45f;
    public AudioClip shotClip;
    public float flashIntensity = 3f;
    public float fadeSpeed = 10f;

    private Animator anim;
    private HashIDs hash;
    public LineRenderer laserShotLine;
    private Light laserShotLight;
    private SphereCollider col;
    private Transform player;
    private PlayerHealth playerHealth;
    private bool shooting;
    private float scaledDamage;

    private float colRadiusRecip;

    void Awake ()
    {
        anim = GetComponent<Animator>();

        player = GameObject.FindGameObjectWithTag(Tags.player).transform;
        //laserShotLine = GetComponentInChildren<LineRenderer>();
        laserShotLight = laserShotLine.GetComponent<Light>();
        col = GetComponent<SphereCollider>();
        player = GameObject.FindGameObjectWithTag(Tags.player).transform;
        playerHealth = player.gameObject.GetComponent<PlayerHealth>();
        hash = GameObject.FindGameObjectWithTag(Tags.gameController).GetComponent<HashIDs>();

        laserShotLine.enabled = false;
        laserShotLight.intensity = 0f;

        colRadiusRecip = 1f / col.radius;

        scaledDamage = maximumDamage - minimumDamage;
    }

    void Update ()
    {
        float shot = anim.GetFloat(hash.shotFloat);
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

    void Shoot ()
    {
        shooting = true;
        float fractionalDistance = (col.radius - Vector3.Distance(transform.position, player.position)) * colRadiusRecip;
        float damage = scaledDamage * fractionalDistance + minimumDamage;
        playerHealth.TakeDamage(damage);
    }
}



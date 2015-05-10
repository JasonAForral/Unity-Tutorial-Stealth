using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerHealth : MonoBehaviour , IDamagable
{
    public SceneFadeInOut sceneFadeInOut;
    
    public float health = 100f;
    public float resetAfterDeathTime = 5f;
    public AudioClip deathClip;

    private Animator anim;
    private PlayerMovement playerMovement;

    private float timer;
    private bool playerDead;

    private AudioSource audioSource;

    public Text healthText;
    public RectTransform healthImageSize;


    public static PlayerHealth instance;

    public static float Health
    {
        get { return instance.health; }
    }

    void Awake ()
    {
        if (null == instance)
            instance = this;
        else if (this != instance)
            Destroy(gameObject);

        anim = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();

        audioSource = GetComponent<AudioSource>();
        UpdateHealthDisplay();
    }

    void Update ()
    {
        if (0 >= health)
        {
            if (!playerDead)
            {
                playerDying();
                health = 0;
                UpdateHealthDisplay();
            }
            else
            {
                PlayerDead();
                LevelReset();
            }
        }
    }

    void playerDying ()
    {
        playerDead = true;
        anim.SetBool(HashIDs.deadBool, true);
        AudioSource.PlayClipAtPoint(deathClip, transform.position);
    }

    void PlayerDead ()
    {
        if (HashIDs.dyingState == anim.GetCurrentAnimatorStateInfo(0).fullPathHash)
        {
            anim.SetBool(HashIDs.deadBool, false);
        }

        anim.SetFloat(HashIDs.speedFloat, 0f);
        playerMovement.enabled = false;
        LastPlayerSighting.position = LastPlayerSighting.resetPosition;
        audioSource.Stop();
    }

    void LevelReset ()
    {
        timer += Time.deltaTime;

        if (timer >= resetAfterDeathTime)
        {
            sceneFadeInOut.EndScene();
        }
    }

    public void TakeDamage (float amount)
    {
        health -= amount;
        UpdateHealthDisplay();
        
    }

    public void UpdateHealthDisplay ()
    {
        healthText.text = Mathf.RoundToInt(health) + System.String.Empty;
        healthImageSize.sizeDelta = new Vector2(health, 10f);
        
    }
}

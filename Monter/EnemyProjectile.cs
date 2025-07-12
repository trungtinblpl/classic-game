using UnityEngine;

public class EnemyProjectile : Enamy_Monter
{
    [SerializeField] private float speed;
    [SerializeField] private float resetTime;
    private float lifetime;
    private Animator anim;
    private BoxCollider2D coll;

    private bool hit;
    // private float direction;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        coll = GetComponent<BoxCollider2D>();
    }

    public void ActivateProjectile()
    {
        hit = false;
        lifetime = 0;
        gameObject.SetActive(true);
        coll.enabled = true;
    }

    private void Update()
    {
        if (hit) return;
        float movementSpeed = speed * Time.deltaTime;
        transform.Translate(movementSpeed, 0, 0);

        lifetime += Time.deltaTime;
        if (lifetime > resetTime)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        hit = true;
        base.OnTriggerEnter2D(collision); //Execute logic from parent script first
        coll.enabled = false;

        if (anim != null)
        {
            // Debug.Log("Animator found. Triggering 'exploreShoot'.");
            anim.SetTrigger("exploreShoot"); //When the object is a fireball explode it
        }
        else
        {
            // Debug.Log("No animator found. Deactivating gameObject.");
            gameObject.SetActive(false); //When this hits any object deactivate arrow
        }

        AudioManager audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        if (audioManager != null && audioManager.kenClip != null)
        {
            audioManager.PlaySFX(audioManager.kenClip);
            // Debug.Log("Ken SFX played.");
        }
    }
    private void Deactivate()
    {
        gameObject.SetActive(false);
    }
}

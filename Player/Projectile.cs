using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float speed;
    private float direction;
    private bool hit;
    private float lifetime;

    private Animator amin;
    private BoxCollider2D boxCollider;

    private void Awake()
    {
        amin = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        if (hit) return;
        float moveSpeed = speed * Time.deltaTime * direction;
        transform.Translate(moveSpeed, 0, 0);

        lifetime += Time.deltaTime;
        if (lifetime > 5) Deactivate();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        hit = true;
        boxCollider.enabled = false;
        amin.SetTrigger("mooLight_Explode");

        AudioManager audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        if (audioManager != null && audioManager.kenClip != null)
        {
            audioManager.PlaySFX(audioManager.kenClip);
            // Debug.Log("Ken SFX played.");
        }

        if (collision.tag == "Monter")
        {
            var (damageToDeal, isCrit) = PlayerStats.Instance.CaculateDame();
            Vector3 center = collision.GetComponent<SpriteRenderer>().bounds.center;
            DameTextManager.Myinstance.CreateText(center, ((int)damageToDeal).ToString(), ColorType.Dame, isCrit);
            collision.GetComponent<Health>()?.TakeDamage(damageToDeal);
        }

        Invoke(nameof(Deactivate), 0.5f);
    }


    public void SetDirection(float _direction)
    {
        lifetime = 0;
        direction = _direction;
        gameObject.SetActive(true);
        hit = false;
        boxCollider.enabled = true;

        float localScaleX = transform.localScale.x;
        if (Mathf.Sign(localScaleX) != _direction)
            localScaleX = -localScaleX;

        transform.localScale = new Vector3(localScaleX, transform.localScale.y, transform.localScale.z);
    }

    private void Deactivate()
    {
        ProjectilePool.Instance.ReturnToPool(this);
    }
}

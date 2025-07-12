
using UnityEngine;

public class fireDog : MonoBehaviour
{
    [Header("Attack Parameters")]
    [SerializeField] private float attackCooldown;
    [SerializeField] private float range;
    [SerializeField] private float damage;

    [Header("Collider Parameters")]
    [SerializeField] private float colliderDistance;
    [SerializeField] private BoxCollider2D boxCollider;

    [Header("Player Layer")]
    [SerializeField] private LayerMask playerLayer;
    private float cooldownTimer = Mathf.Infinity;

    private Animator amin;
    private Health playerHealth;
    private MonterPatrol monterPatrol;

    private void Awake()
    {
        amin = GetComponent<Animator>();
        monterPatrol = GetComponentInParent<MonterPatrol>();
    }

    private void Update()
    {
        cooldownTimer += Time.deltaTime;

        if (PlayerInsight())
        {
            if (monterPatrol != null)
            {
                monterPatrol.enabled = false; // Dừng hệ thống tuần tra ngay lập tức
            }

            if (cooldownTimer >= attackCooldown)
            {
                cooldownTimer = 0;
                amin.SetTrigger("fireDogAttack"); // Tấn công người chơi
            }
        }
        else
        {
            if (monterPatrol != null)
            {
                monterPatrol.enabled = !PlayerInsight(); // Bật lại tuần tra nếu không thấy người chơi
            }
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (((1 << collision.gameObject.layer) & playerLayer) != 0)
        {
            playerHealth = collision.GetComponent<Health>();
            if (playerHealth != null)
            {
                cooldownTimer = 0;
                amin.SetTrigger("fireDogAttack");
            }
        }

        AudioManager audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        if (audioManager != null && audioManager.enemyAttackClip != null)
        {
            audioManager.PlaySFX(audioManager.enemyAttackClip);
            // Debug.Log("EnemiesAttack SFX played.");
        }
    }

    private bool PlayerInsight()
    {
        Vector2 direction = transform.localScale.x > 0 ? Vector2.right : Vector2.left;
        RaycastHit2D hit = Physics2D.BoxCast(
            boxCollider.bounds.center + (Vector3)direction * range * colliderDistance,
            new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z),
            0, direction, 0, playerLayer
        );

        if (hit.collider != null)
        {
            playerHealth = hit.transform.GetComponent<Health>();
        }
        return hit.collider != null;
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right
        * range * transform.localScale.x * colliderDistance,
        new Vector3(boxCollider.bounds.size.x
        * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z));
    }

    public void DamagePlayer()
    {
        if (PlayerInsight())
        {
            playerHealth.TakeDamage(damage);
        }
    }

}



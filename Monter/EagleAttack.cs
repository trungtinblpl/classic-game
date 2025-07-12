using UnityEngine;
using System.Collections;

public class EagleAttack : MonoBehaviour
{
    public Rigidbody2D rb;
    public float diveSpeed = 10f;
    public float resetDelay = 2f;
    public int damage = 1;

    public float attackCooldown = 3f; // thời gian cooldown 
    private float cooldownTimer = 0f;

    private Animator animator;

    private Vector2 startPos;
    private bool isAttacking = false;
    private bool canAttack = true;

    private void Start()
    {
        startPos = transform.position;
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        // cooldown
        if (!canAttack)
        {
            cooldownTimer -= Time.deltaTime;
            if (cooldownTimer <= 0f)
            {
                canAttack = true;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !isAttacking && canAttack)
        {
            isAttacking = true;
            canAttack = false; // khóa lại ngay khi bắt đầu attack
            cooldownTimer = attackCooldown; // bắt đầu tính cooldown

            // Hướng dive
            float limitedDiveSpeed = diveSpeed;
            Vector2 direction = (collision.transform.position - transform.position).normalized;
            float limitedY = Mathf.Clamp(direction.y, -0.5f, -1f);

            rb.velocity = new Vector2(direction.x * limitedDiveSpeed, limitedY * limitedDiveSpeed);

            animator.SetTrigger("Dive");

            AudioManager audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
            if (audioManager != null && audioManager.eagleAttackClip != null)
            {
                audioManager.PlaySFX(audioManager.eagleAttackClip);
                // Debug.Log("EagleAttack SFX played.");
            }

            Health health = collision.GetComponent<Health>();
            if (health != null)
            {
                health.TakeDamage(damage);
            }

            StartCoroutine(ResetEagle());
        }
    }

    private IEnumerator ResetEagle()
    {
        yield return new WaitForSeconds(resetDelay);

        rb.velocity = Vector2.zero;
        transform.position = startPos;

        animator.ResetTrigger("Dive");

        isAttacking = false;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireTrap : MonoBehaviour
{
    [SerializeField] private float damage = 10f;

    [Header("FireTrap Settings")]
    [SerializeField] private float activationDelay = 1f;
    [SerializeField] private float activeTime = 1f;
    [SerializeField] private float cycleInterval = 3f;
    [SerializeField] private float damageCooldown = 1f; // Gây damage mỗi 1 giây

    private Animator anim;
    private SpriteRenderer spriteRenderer;
    private bool active;

    private Dictionary<GameObject, float> lastDamageTime = new Dictionary<GameObject, float>();

    private void Awake()
    {
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        StartCoroutine(FireTrapCycle());
    }

    private IEnumerator FireTrapCycle()
    {
        while (true)
        {
            spriteRenderer.color = Color.red;
            yield return new WaitForSeconds(activationDelay);

            spriteRenderer.color = Color.white;
            active = true;
            anim.SetBool("active", true);

            yield return new WaitForSeconds(activeTime);

            active = false;
            anim.SetBool("active", false);

            yield return new WaitForSeconds(cycleInterval);
        }
    }

    private void OnTriggerStay2D(Collider2D collider)
    {
        if (active && collider.CompareTag("Player"))
        {
            GameObject player = collider.gameObject;
            float currentTime = Time.time;

            // Nếu lần trước gây damage đã qua cooldown thì mới gây tiếp
            if (!lastDamageTime.ContainsKey(player) || currentTime - lastDamageTime[player] >= damageCooldown)
            {
                Health health = player.GetComponent<Health>();
                if (health != null)
                {
                    health.TakeDamage(damage);
                    lastDamageTime[player] = currentTime;
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            lastDamageTime.Remove(collider.gameObject); // Reset timer khi ra khỏi vùng
        }
    }
}

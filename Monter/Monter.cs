using System.Collections;
using UnityEngine;

public class Monter : MonoBehaviour
{
    public int maxHealth = 0;
    public int currentHealth;
    private Animator anim;
    private bool isDead = false;
    private Health health;

    void Start()
    {
        anim = GetComponent<Animator>();
        health = GetComponent<Health>();
    }

    public void TakeDamage(int damage)
    {
        if (health != null)
            health.TakeDamage(damage);
    }


    void Die()
    {
        if (!isDead) return;
        isDead = true;

        // Chạy animation chết
        anim.SetTrigger("die");

        // Tắt Collider để tránh va chạm
        GetComponent<Collider2D>().enabled = false;

        // Xóa quái vật sau khi animation kết thúc
        StartCoroutine(DestroyAfterAnimation());
    }

    IEnumerator DestroyAfterAnimation()
    {
        yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length);
        Destroy(gameObject);
    }
}

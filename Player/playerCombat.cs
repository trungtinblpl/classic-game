using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerCombat : MonoBehaviour
{
    public Animator animator;

    public Transform attackPoint;
    public LayerMask enemyLayers;
    public float attackRange = 0.5f;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            Attack();
        }
    }

    private void Attack()
    {
        animator.SetTrigger("Attack");

        bool hitSuccessful = false;

        // Detect enemies
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        foreach (Collider2D enemy in hitEnemies)
        {
            if (enemy != null && enemy.GetComponent<Health>() != null)
            {
                hitSuccessful = true;

                var (damageToDeal, isCrit) = PlayerStats.Instance.CaculateDame();

                Vector3 enemyCenter = enemy.GetComponent<SpriteRenderer>().bounds.center;
                DameTextManager.Myinstance.CreateText(enemyCenter, ((int)damageToDeal).ToString(), ColorType.Dame, isCrit);

                enemy.GetComponent<Health>().TakeDamage(damageToDeal);
            }
        }

        if (hitSuccessful)
        {
            AudioManager audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
            if (audioManager != null && audioManager.kenClip != null)
            {
                audioManager.PlaySFX(audioManager.kenClip);
                // Debug.Log("Ken SFX played.");
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}

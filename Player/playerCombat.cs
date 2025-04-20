using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerCombat : MonoBehaviour
{

    public Animator animator;

    public Transform attackPoint;
    public LayerMask enemyLayers;

    public float attackRange = 0.5f;
    public int attackDamage = 10;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F)){
            Attack();
        }
    }

    void Attack(){
        //play an attack animation
        animator.SetTrigger("Attack");


        //Detect monter attack
        Collider2D[] hitMonters = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);


        //dame
        foreach (Collider2D enemy in hitMonters)
        {
            if (enemy != null && enemy.GetComponent<Monter>() != null) // Kiểm tra null trước khi gọi hàm
            {
                enemy.GetComponent<Monter>().TakeDamage(attackDamage);
            }
        }

    }

    void OnDrawGizmosSelected(){
        if(attackPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapDame : MonoBehaviour
{
    [SerializeField] private float damage;

    protected virtual void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            collider.gameObject.GetComponent<Health>().TakeDamage(damage);
        }
    }
}

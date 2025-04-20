
using UnityEngine;

public class Enamy_Monter : MonoBehaviour
{
    // [SerializeField] private float damaege;

    // private void OnTriggerCollisionEnter2D(Collider2D collision){
    //     if(collision.tag == "Player"){
    //         collision.GetComponent<Health>().TakeDamage(damaege);
    //     }
    // }
    [SerializeField] protected float damage;

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
            collision.GetComponent<Health>().TakeDamage(damage);
    }
}

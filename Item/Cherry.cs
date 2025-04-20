// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class Cherry : MonoBehaviour
// {
//     private void OnTriggerEnter2D(Collider2D other){
//         if(other.CompareTag("Player")){
//             int effect = Random.Range(0, 3);
//             Health playerHealth = other.GetComponent<Health>();

//             switch(effect){
//                 case 0:
//                     playerHealth.AddHealthPercent(0.2f);
//                     Debug.Log("<color=green>🍒 Cherry effect: Heal 20% HP</color>");
//                     break;

//                 case 1:
//                     PlayerStats.Instance.BuffDamage(5f);
//                     Debug.Log("<color=red>🍒 Cherry effect: Damage up for 5s</color>");
//                     StartCoroutine(RemoveDamageBuff(5f));
//                     break;

//                 case 2:
//                     PlayerStats.Instance.BuffCritChance(10f);
//                     Debug.Log("<color=yellow>🍒 Cherry effect: Crit +10%</color>");
//                     StartCoroutine(RemoveCritBuff(5f));
//                     break;      
//             }
//             Destroy(gameObject);
//         }
//     }

//     private IEnumerator RemoveDamageBuff(float duration){
//         Debug.Log($"Damage buff started, waiting to remove... at {Time.time}");
//         yield return new WaitForSeconds(duration);
//         Debug.Log($"5s passed at {Time.time} — Removing buff now");
//         PlayerStats.Instance.BuffDamage(-5f);
//         Debug.Log($"Damage buff removed at {Time.time}, New damage: {PlayerStats.Instance.damage}");
//     }

//         private IEnumerator RemoveCritBuff(float duration){
//         Debug.Log("Crit chance buff started, waiting to remove...");
//         yield return new WaitForSeconds(duration);
//         PlayerStats.Instance.BuffCritChance(-10f);
//         Debug.Log("<color=yellow>🍒 Cherry effect: Crit chance buff ended</color>");    }
// }

using System.Collections;
using UnityEngine;

public class Cherry : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            int effect = Random.Range(0, 3);
            Health playerHealth = other.GetComponent<Health>();

            switch (effect)
            {
                case 0:
                    playerHealth.AddHealthPercent(0.2f);
                    Debug.Log("<color=green>🍒 Cherry effect: Heal 20% HP</color>");
                    break;

                case 1:
                    PlayerStats.Instance.TemporaryBuff(PlayerStats.Instance.BuffDamage, 5f, 5f);
                    Debug.Log("<color=red>🍒 Cherry effect: Damage up for 5s</color>");
                    break;

                case 2:
                    PlayerStats.Instance.TemporaryBuff(PlayerStats.Instance.BuffCritChance, 10f, 5f);
                    Debug.Log("<color=yellow>🍒 Cherry effect: Crit +10% for 5s</color>");
                    break;
            }

            Destroy(gameObject);
        }
    }
}

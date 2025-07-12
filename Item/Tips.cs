// using UnityEngine;

// public class Tips : MonoBehaviour, ICollectible
// {
//     public void Collect(GameObject collector)
//     {
//         PlayerInventory inv = collector.GetComponent<PlayerInventory>();
//         inv.AddTips(1);

//         AudioManager audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
//         if (audioManager != null && audioManager.itemsClip != null)
//         {
//             audioManager.PlaySFX(audioManager.itemsClip);
//             Debug.Log("Gem SFX played.");
//         }

//         Animator anim = GetComponent<Animator>();
//         if (anim) anim.SetTrigger("Collected");

//         Destroy(gameObject, 0.5f);
//     }
// }

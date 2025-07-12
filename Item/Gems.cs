using UnityEngine;

public class Gems : MonoBehaviour, ICollectible
{
    public void Collect(GameObject collector)
    {
        PlayerInventory ivn = collector.GetComponent<PlayerInventory>();
        ivn.AddGems(1);

        AudioManager audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        if (audioManager != null && audioManager.itemsClip != null)
        {
            audioManager.PlaySFX(audioManager.itemsClip);
            // Debug.Log("Gem SFX played.");
        }

        Animator amin = GetComponent<Animator>();
        if (amin) amin.SetTrigger("Collected");

        Destroy(gameObject, 0.5f);
    }
}

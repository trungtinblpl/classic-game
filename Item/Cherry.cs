using System.Collections;
using UnityEngine;

public class Cherry : MonoBehaviour
{
    private Animator amin;
    private bool isPickedUp = false;
    public GameObject textBuffPrefab;
    private AudioManager audioManager;

    private void Awake()
    {
        amin = GetComponent<Animator>();
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!isPickedUp && other.CompareTag("Player"))
        {
            audioManager.PlaySFX(audioManager.itemsClip);

            isPickedUp = true;

            int effect = Random.Range(0, 3);
            Health playerHealth = other.GetComponent<Health>();

            Vector3 spawnPos = other.transform.position + new Vector3(0, 1.5f, 0);

            // Text và màu tương ứng
            string buffText = "";
            Color buffColor = Color.white;

            switch (effect)
            {
                case 0:
                    playerHealth.AddHealthPercent(0.2f);
                    buffText = "Healed +20%";
                    buffColor = Color.green;
                    // Debug.Log("<color=green>🍒 Cherry effect: Heal 20% HP</color>");
                    break;

                case 1:
                    PlayerStats.Instance.TemporaryBuff(PlayerStats.Instance.BuffDamage, 5f, 5f);
                    buffText = "Damage Up!";
                    buffColor = Color.red;
                    // Debug.Log("<color=red>🍒 Cherry effect: Damage up for 5s</color>");
                    break;

                case 2:
                    PlayerStats.Instance.TemporaryBuff(PlayerStats.Instance.BuffCritChance, 10f, 5f);
                    buffText = "Crit +10%";
                    buffColor = Color.yellow;
                    // Debug.Log("<color=yellow>🍒 Cherry effect: Crit +10% for 5s</color>");
                    break;
            }

            amin.SetTrigger("Picked");

            // Vô hiệu hóa collider để không bị nhặt lại
            GetComponent<Collider2D>().enabled = false;

            // Tạm hoãn hủy object sau animation
            StartCoroutine(DestroyAfterAnimation());

            if (textBuffPrefab != null)
            {
                GameObject textObj = Instantiate(textBuffPrefab, spawnPos, Quaternion.identity);
                textObj.GetComponent<FloatingText>().SetText(buffText, buffColor);
            }
        }
    }
    private IEnumerator DestroyAfterAnimation()
    {
        yield return new WaitForSeconds(0.5f); // Thời gian animation chạy
        Destroy(gameObject);
    }
}

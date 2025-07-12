using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    [SerializeField] private Image totalhealthBar;
    [SerializeField] private Image currenthealthBar;
    private Health targetHealth;

    private void Start()
    {
        totalhealthBar.fillAmount = 1f;

        // Nếu chưa gán trong Inspector, tự tìm trong Player
        if (targetHealth == null)
        {
            targetHealth = GameObject.FindWithTag("Player")?.GetComponent<Health>();

            // if (targetHealth == null)
            // {
            //     Debug.LogWarning("Healthbar: Không tìm thấy Player hoặc Health script.");
            // }
        }
    }

    private void Update()
    {
        // Nếu chưa tìm thấy targetHealth, thử tìm lại
        if (targetHealth == null)
        {
            GameObject playerObj = GameObject.FindWithTag("Player");
            if (playerObj != null)
            {
                targetHealth = playerObj.GetComponent<Health>();
                // Debug.Log("Healthbar đã kết nối với Player mới.");
            }
            else
            {
                return; // Không có player, không làm gì tiếp
            }
        }

        // Cập nhật thanh máu
        if (targetHealth.maxHealth > 0)
        {
            currenthealthBar.fillAmount = targetHealth.currentHealth / (float)targetHealth.maxHealth;
        }
    }


    // private void Update()
    // {
    //     if (targetHealth != null && targetHealth.maxHealth > 0)
    //     {
    //         currenthealthBar.fillAmount = targetHealth.currentHealth / (float)targetHealth.maxHealth;
    //     }
    // }
}


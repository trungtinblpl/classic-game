using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public enum ColorType { Dame, Crit }
public class DameTextManager : MonoBehaviour
{
    private static DameTextManager Instance;

    public static DameTextManager Myinstance {
        get {
            if (Instance == null){
                Instance = FindObjectOfType<DameTextManager>();
            }
            return Instance;
        }
    }

    [SerializeField] private GameObject DameTextPrefab;
    [SerializeField] private Sprite critIconSprite;  // Sprite cho biểu tượng chí mạng
    // [SerializeField] private float critChance = 0.2f;  // 20% cơ hội chí mạng

    public void CreateText(Vector2 position, string text, ColorType type, bool isCrit = false){
        TextMeshProUGUI typeText = Instantiate(DameTextPrefab, transform).GetComponent<TextMeshProUGUI>();
        typeText.transform.position = position;
        string sign = string.Empty;
        Color textColor = Color.white;

        // Thêm icon chí mạng nếu là chí mạng
        DameText dameTextComponent = typeText.GetComponent<DameText>();

        if (isCrit) {
            sign = "CRIT! ";
            textColor = Color.red;  // Màu vàng cho chí mạng

            // if (dameTextComponent != null) {
            //     dameTextComponent.SetCritIcon(critIconSprite, true);  // Hiển thị biểu tượng chí mạng
            // }
        } else {
            sign = "-";
            textColor = Color.red;  // Màu đỏ cho sát thương bình thường

            // if (dameTextComponent != null) {
            //     dameTextComponent.SetCritIcon(critIconSprite, false);  // Ẩn biểu tượng chí mạng
            // }
        }

        typeText.color = textColor;
        typeText.text = sign + text;
    }

    // Hàm để kiểm tra xem có phải chí mạng không
    public bool IsCriticalHit() {
        return Random.value < PlayerStats.Instance.critChance / 100f;
    }


    // Hàm để xử lý tạo thông báo sát thương với chí mạng
    public void DealDamage(Vector2 position, int damage) {
        bool isCrit = IsCriticalHit();  // Kiểm tra có phải chí mạng không
        int finalDamage = isCrit ? damage * 2 : damage;  // Sát thương chí mạng nhân đôi
        CreateText(position, finalDamage.ToString(), ColorType.Dame, isCrit);  // Tạo thông báo sát thương
    }
}


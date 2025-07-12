using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public enum ColorType { Dame, Crit }
public class DameTextManager : MonoBehaviour
{
    private static DameTextManager Instance;

    public static DameTextManager Myinstance
    {
        get
        {
            if (Instance == null)
            {
                Instance = FindObjectOfType<DameTextManager>();
            }
            return Instance;
        }
    }

    [SerializeField] private GameObject DameTextPrefab;
    // [SerializeField] private Sprite critIconSprite;

    public void CreateText(Vector2 position, string text, ColorType type, bool isCrit = false)
    {
        TextMeshProUGUI typeText = Instantiate(DameTextPrefab, transform).GetComponent<TextMeshProUGUI>();
        typeText.transform.position = position;

        string sign = isCrit ? "CRIT " : "-";
        Color textColor = Color.red;

        typeText.color = textColor;
        typeText.text = sign + text;
        // Debug.Log((isCrit ? "Chí mạng" : "Không phải chí mạng") + ": " + sign + text);
    }
}


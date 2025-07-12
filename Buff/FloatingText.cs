using UnityEngine;
using TMPro;

public class FloatingText : MonoBehaviour
{
    public float lifetime = 2f;
    public Vector3 floatOffset = new Vector3(0, 1f, 0);

    private void Start()
    {
        // Tự động di chuyển nhẹ lên và biến mất
        transform.position += floatOffset;
        Destroy(gameObject, lifetime);
    }

    public void SetText(string text, Color color)
    {
        var tmp = GetComponentInChildren<TMP_Text>();
        if (tmp != null)
        {
            tmp.text = text;
            tmp.color = color;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;  // Để sử dụng Image

public class DameText : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private TextMeshProUGUI Text;
    [SerializeField] private float lifeTime;
    // [SerializeField] public Image critIcon;  // Biểu tượng chí mạng
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Fadeout());
    }

    // Update is called once per frame
    void Update()
    {
        MoveText();
    }

    public void MoveText(){
        transform.Translate(Vector2.up * speed * Time.deltaTime);
    }

    public IEnumerator Fadeout(){
        float startAlpha = Text.color.a;
        float rate = 1.0f / lifeTime;
        float progress = 0.0f;

        while (progress < 1.0f){
            Color tmp = Text.color;
            tmp.a = Mathf.Lerp(startAlpha, 0, progress);

            Text.color = tmp;

            progress += rate * Time.deltaTime;
            yield return null;
        }
        Destroy(gameObject);
    }

    // Hàm này có thể được gọi từ DameTextManager để điều chỉnh icon chí mạng
    // public void SetCritIcon(Sprite critSprite, bool isCrit) {
    //     if (critIcon != null) {
    //         critIcon.sprite = critSprite;  // Cập nhật biểu tượng chí mạng
    //         critIcon.enabled = isCrit;  // Hiển thị hoặc ẩn biểu tượng dựa vào isCrit
    //     }
    // }
}

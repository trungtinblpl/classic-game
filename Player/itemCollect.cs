using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System.Collections.Generic;
using System.Collections;
using TMPro;

public class itemCollect : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        ICollectible item = collision.GetComponent<ICollectible>();
        if (item != null)
        {
            item.Collect(gameObject);  // Giao cho vật phẩm tự xử lý
        }
    }
}

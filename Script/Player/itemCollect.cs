// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.UI;

// public class itemCollect : MonoBehaviour
// {
//     private int tips = 0;

//     [SerializeField] private Text tipsText;

//     private void OnTriggerEnter2D(Collider2D collision){
//         if(collision.gameObject.CompareTag("Tips")){
//             Destroy(collision.gameObject);
//             tips++;
//             Debug.Log("Tips: " + tips);
//             tipsText.text = "Tips " + tips;
//         }
//     }


// }

using UnityEngine;
using UnityEngine.UI;
using System.Linq;    
using System.Collections.Generic;
using System.Collections;

public class itemCollect : MonoBehaviour
{
    [SerializeField] private Text tipsText;
    [SerializeField] private Text messageText;

    private bool collectedFirstItem = false;
    private int tips = 0;


    private List<GameObject> collectedItems = new List<GameObject>();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Tips"))
        {
            // Xử lý khi ăn được vật phẩm thứ nhất
            collectedFirstItem = true;
            Destroy(collision.gameObject);
            collectedItems.Add(collision.gameObject);
            SortCollectedItemsByPriority();
            UpdateTipsCount();

        } else if (collision.gameObject.CompareTag("Tips 1")){
            if (collectedFirstItem){
                Destroy(collision.gameObject);
                collectedItems.Add(collision.gameObject);
                SortCollectedItemsByPriority();
                UpdateTipsCount();
            } else {
                DisplayMessage("Bạn chưa  ký học môn Lập trình căn bản, nên bạn không thể đăng ký học môn cấu trúc dữ liệu được");
                Debug.Log("Môn Cấu trúc dữ liệu không thể đăng ký được.");
            }
        }
    }

    private void SortCollectedItemsByPriority()
    {
        List<GameObject> validItems = collectedItems.Where(item => item != null).ToList();
        validItems.Sort((item1, item2) => item1.transform.position.x.CompareTo(item2.transform.position.x));
        collectedItems = validItems;
    }

    private void UpdateTipsCount()
    {
        //Tăng điểm
        tips++;
        Debug.Log("Score: " + tips);
        tipsText.text = "Score " + tips;
    }

    private void DisplayMessage(string message)
    {
        messageText.text = message;
        StartCoroutine(ClearMessageAfterDelay(3f)); // Xóa thông báo sau 3 giây
    }

    private IEnumerator ClearMessageAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        messageText.text = "";
    }
}


using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class Message : MonoBehaviour
{
    [SerializeField] private Text messageText;

    private void Start()
    {
        // Đảm bảo messageText đã được gán trước khi sử dụng
        if (messageText == null)
        {
            Debug.LogError("Not eat");
            return;
        }
        // Ẩn thông báo khi bắt đầu
        messageText.text = "";
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

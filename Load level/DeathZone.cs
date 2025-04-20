using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathZone : MonoBehaviour
{
    public GameObject gameOverUI;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // Kiểm tra nếu đối tượng là nhân vật
        {
            Debug.Log("Game Over!");
            GameOver(); // Gọi hàm Game Over
        }
    }

    void GameOver()
    {
        if (gameOverUI != null)
        {
            gameOverUI.SetActive(true);
        }
        else
        {
            Debug.LogError("gameOverUI chưa được gán!");
        }

        // Ngừng thời gian nếu cần
        Time.timeScale = 0f; 
    }
}

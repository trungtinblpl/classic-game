using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathZone : MonoBehaviour
{
    public GameObject gameOverUI;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Debug.Log("Game Over!");
            GameOver(); // Gọi hàm Game Over
        }
    }

    void GameOver()
    {
        if (gameOverUI != null)
        {
            gameOverUI.SetActive(true);
        }
        // else
        // {
        //     // Debug.LogError("gameOverUI chưa được gán!");
        // }

        AudioManager audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        if (audioManager != null)
        {
            audioManager.PlayerLoseMusic();
            // Debug.Log("loseClip SFX played.");
        }

        // Ngừng thời gian
        Time.timeScale = 0f;
    }
}

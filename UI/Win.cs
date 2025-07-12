using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Win : MonoBehaviour
{
    [Header("Game Win")]
    [SerializeField] public GameObject gameWinScreen;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (gameWinScreen != null)
            {
                gameWinScreen.SetActive(true);

                AudioManager audioManager = GameObject.FindGameObjectWithTag("Audio")?.GetComponent<AudioManager>();
                if (audioManager != null && audioManager.winClip != null)
                {
                    audioManager.PlayMusic(audioManager.winClip); // Hoặc PlaySFX nếu chỉ là âm ngắn
                }
            }

            Time.timeScale = 0f;

        }
    }

}

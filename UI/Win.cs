using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Win : MonoBehaviour
{
    [Header ("Game Win")]
    [SerializeField] public GameObject gameWinScreen;

    private void OnTriggerEnter2D(Collider2D collision){
        if(collision.CompareTag("Player")){
            // collision.CompareTag.SetActive(false);
            if (gameWinScreen != null)
            {
                gameWinScreen.SetActive(true);
            }

            Time.timeScale = 0f;
        }
    }
}

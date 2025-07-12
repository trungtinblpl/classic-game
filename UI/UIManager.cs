
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [Header("Game Win")]
    [SerializeField] public GameObject gameWinScreen;

    [Header("Game Over")]
    [SerializeField] private GameObject gameOverScreen;

    [Header("Pause")]
    [SerializeField] private GameObject pauseScreen;
    [SerializeField] GameObject pauseButton;

    private void Awake()
    {
        // gameWinScreen.SetActive(false);
        // gameOverScreen.SetActive(false);
        // pauseScreen.SetActive(false);
        gameWinScreen?.SetActive(false);
        gameOverScreen?.SetActive(false);
        pauseScreen?.SetActive(false);
        pauseButton?.SetActive(true);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // if (pauseScreen.activeInHierarchy)
            //     PauseGame(false);
            // else
            //     PauseGame(true);

            TogglePauseButton();
        }
    }

    #region Game Over Functions
    //Game over function
    public void GameOver()
    {
        gameOverScreen.SetActive(true);
        Time.timeScale = 0f;

        AudioManager audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        if (audioManager != null)
        {
            audioManager.PlayerLoseMusic();
            // Debug.Log("loseClip SFX played.");
        }
    }

    //Restart level
    public void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }

    //Quit game/exit play mode if in Editor
    public void Quit()
    {
        Application.Quit(); //Quits the game (only works in build)

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; //Exits play mode
#endif
    }
    #endregion

    #region Pause
    public void PauseGame(bool isPaused)
    {
        if (pauseScreen != null) pauseScreen.SetActive(isPaused);
        if (pauseButton != null) pauseButton.SetActive(!isPaused);

        Time.timeScale = isPaused ? 0f : 1f;

        // pauseScreen.SetActive(status);
        // PauseButton.SetActive(false);

        // if (status)
        //     Time.timeScale = 0;
        // else
        //     Time.timeScale = 1;
    }

    //nút Pause
    public void TogglePauseButton()
    {
        bool isPaused = pauseScreen != null && pauseScreen.activeInHierarchy;
        PauseGame(!isPaused);
    }
    #endregion

    public void SFXVolume()
    {
        AudioManager.Instance.ChangeSFXVolume(0.2f);
    }

    public void MusicVolume()
    {
        AudioManager.Instance.ChangeMusicVolume(0.2f);
    }

}

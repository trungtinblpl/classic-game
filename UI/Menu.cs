using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [Header("New Game")]
    [SerializeField] private GameObject gameNewScreen;

    [Header("Setting")]
    [SerializeField] private GameObject settingPanel;

    [Header("Store")]
    [SerializeField] private GameObject storePanel;

    [Header("Intruct")]
    [SerializeField] private GameObject intrustPanel;

    public void NewGameScreen()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(1);
    }

    #region Settings Functions
    public void ShowSetting()
    {
        settingPanel.SetActive(true);
    }

    public void CloseSetting()
    {
        settingPanel.SetActive(false);
    }
    #endregion

    #region Store Functions
    public void ShowStore()
    {
        storePanel.SetActive(true);
    }

    public void CloseStore()
    {
        storePanel.SetActive(false);
    }
    #endregion

    #region Intruct Functions
    public void ShowIntruct()
    {
        intrustPanel.SetActive(true);
    }

    public void CloseIntruct()
    {
        intrustPanel.SetActive(false);
    }
    #endregion

    #region New Game Functions
    public void Quit()
    {
        Application.Quit(); //Quits the game (only works in build)

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; //Exits play mode
#endif
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

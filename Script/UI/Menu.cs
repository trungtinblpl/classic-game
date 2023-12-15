
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [Header ("New Game")]
    [SerializeField] private GameObject gameNewScreen;

    public void NewGameScreen(){
        SceneManager.LoadScene(1);
    }

    #region New Game Functions
    public void Quit()
    {
        Application.Quit(); //Quits the game (only works in build)

        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; //Exits play mode
        #endif
    }
    #endregion
}

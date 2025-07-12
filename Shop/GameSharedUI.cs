using UnityEngine;
using TMPro;

public class GameSharedUI : MonoBehaviour
{
    #region Singleton class: GameSharedUI

    public static GameSharedUI Instance;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    #endregion

    [SerializeField] TMP_Text[] gemsUIText;

    void Start()
    {
        UpdateGemsUIText();
    }

    public void UpdateGemsUIText()
    {
        for (int i = 0; i < gemsUIText.Length; i++)
        {
            SetGemsText(gemsUIText[i], GameDataManager.GetGems());
        }
    }

    void SetGemsText(TMP_Text textMesh, int value)
    {

        if (value >= 1000)
            textMesh.text = string.Format("{0}K.{1}", (value / 1000), GetFirstDigitFromNumber(value % 1000));
        else
            textMesh.text = value.ToString();
    }

    int GetFirstDigitFromNumber(int num)
    {
        return int.Parse(num.ToString()[0].ToString());
    }
}
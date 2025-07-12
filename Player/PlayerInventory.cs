using UnityEngine;
using TMPro;

public class PlayerInventory : MonoBehaviour
{
    public int gems;
    // public int tips;

    [SerializeField] private TextMeshProUGUI gemsText;
    // [SerializeField] private TextMeshProUGUI tipsText;

    private void Start()
    {
        gems = GameDataManager.GetGems();
        // tips = GameDataManager.GetTips();

        UpdateUI();
    }

    public void AddGems(int amount)
    {
        gems += amount;
        gemsText.text = gems.ToString();
        GameDataManager.AddGems(amount);
    }

    // public void AddTips(int amount)
    // {
    //     tips += amount;
    //     tipsText.text = tips.ToString();
    //     GameDataManager.SetTips(tips);
    // }

    public void UpdateUI()
    {
        gemsText.text = gems.ToString();
        // tipsText.text = tips.ToString();
    }
}

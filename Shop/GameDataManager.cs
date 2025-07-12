using System.Collections.Generic;

[System.Serializable]
public class CharactersShopData
{
    public List<int> purchasedCharactersIndexes = new List<int>();
}

[System.Serializable]
public class PlayerData
{
    public int gems = 0;
    public int selectedCharacterIndex = 0;
}

public class GameDataManager
{
    static PlayerData playerData = new PlayerData();
    static CharactersShopData charactersShopData = new CharactersShopData();
    static CharacterData selectedCharacter;

    static GameDataManager()
    {
        LoadPlayerData();
        LoadCharactersShopData();
    }

    //Player Data Methods -----------------------------------------------------------------------------
    public static CharacterData GetSelectedCharacter()
    {
        return selectedCharacter;
    }

    public static void SetSelectedCharacter(CharacterData characterdb, int index)
    {
        selectedCharacter = characterdb;
        playerData.selectedCharacterIndex = index;
        SavePlayerData();
    }

    public static int GetSelectedCharacterIndex()
    {
        return playerData.selectedCharacterIndex;
    }

    public static int GetGems()
    {
        return playerData.gems;
    }

    public static void AddGems(int amount)
    {
        playerData.gems += amount;
        SavePlayerData();
    }

    public static bool CanSpendGems(int amount)
    {
        return (playerData.gems >= amount);
    }

    public static void SpendGems(int amount)
    {
        playerData.gems -= amount;
        SavePlayerData();
    }

    static void LoadPlayerData()
    {
        playerData = BinarySerializer.Load<PlayerData>("player-data.txt");
        // UnityEngine.Debug.Log("<color=green>[PlayerData] Loaded.</color>");
    }

    static void SavePlayerData()
    {
        BinarySerializer.Save(playerData, "player-data.txt");
        // UnityEngine.Debug.Log("<color=magenta>[PlayerData] Saved.</color>");

    }

    //Characters Shop Data Methods -----------------------------------------------------------------------------
    public static void AddPurchasedCharacter(int characterIndex)
    {
        charactersShopData.purchasedCharactersIndexes.Add(characterIndex);
        SaveCharactersShopData();
    }

    public static List<int> GetAllPurchasedCharacter()
    {
        return charactersShopData.purchasedCharactersIndexes;
    }

    public static int GetPurchasedCharacter(int index)
    {
        return charactersShopData.purchasedCharactersIndexes[index];
    }

    static void LoadCharactersShopData()
    {
        charactersShopData = BinarySerializer.Load<CharactersShopData>("characters-shop-data.txt");
        // UnityEngine.Debug.Log("<color=green>[CharactersShopData] Loaded.</color>");
    }

    static void SaveCharactersShopData()
    {
        BinarySerializer.Save(charactersShopData, "characters-shop-data.txt");
        // UnityEngine.Debug.Log("<color=magenta>[CharactersShopData] Saved.</color>");
    }
}


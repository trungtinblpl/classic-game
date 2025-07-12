using UnityEngine;

[CreateAssetMenu(fileName = "CharacterShopManager", menuName = "Shopping/Character shop database")]
public class CharacterShopManager : ScriptableObject
{
    public CharacterData[] characters;

    public int CharacterCount
    {
        get { return characters.Length; }
    }

    public CharacterData GetCharacter(int index)
    {
        return characters[index];
    }

    public void PurchaseCharacter(int index)
    {
        characters[index].isPurchased = true;
    }
}

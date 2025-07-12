using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class CharacterShopUI : MonoBehaviour
{
    [Header("Layout Setting")]
    [SerializeField] float itemSpacing = .5f;
    float itemHeight;

    [Space(20f)]
    [Header("UI element")]
    [SerializeField] Transform ShopMenu;
    [SerializeField] Transform ShopItemsContainer;
    [SerializeField] Transform itemPrefab;

    [Space(20f)]
    [SerializeField] CharacterShopManager characterDB;

    [Space(20f)]
    [Header("Shop Events")]
    [SerializeField] GameObject shopUI;
    [SerializeField] Button openShopButton;
    [SerializeField] Button closeShopButton;

    [Space(20f)]
    [Header("Scroll View")]
    [SerializeField] ScrollRect scrollRect;

    [Space(20f)]
    [Header("Purchase Fx & Messages")]
    [SerializeField] ParticleSystem purchaseFx;
    [SerializeField] Transform purchaseFxPos;
    [SerializeField] TMP_Text noEnoughGemsText;

    int newSelectedItemIndex = 0;
    int priviousSelectedItemIndex = 0;

    void Start()
    {
        purchaseFx.transform.position = purchaseFxPos.position;

        AddShopEvents();
        GenerateShopItemsUI();

        //set selected character in the playerDataManager
        SetSelectedCharacter();

        //select UI item
        SelectItemUI(GameDataManager.GetSelectedCharacterIndex());

        //update player skin
        ChangePlayerSkin();

        //Auto scroll to selected chracter
        AutoScrollShopList(GameDataManager.GetSelectedCharacterIndex());
    }

    void AutoScrollShopList(int itemIndex)
    {
        scrollRect.verticalNormalizedPosition =
        Mathf.Clamp01(1f - (itemIndex / (float)(characterDB.CharacterCount - 1)));
    }

    void SetSelectedCharacter()
    {
        //get save index
        int index = GameDataManager.GetSelectedCharacterIndex();

        //Set selected character
        GameDataManager.SetSelectedCharacter(characterDB.GetCharacter(index), index);
    }

    void GenerateShopItemsUI()
    {

        for (int i = 0; i < GameDataManager.GetAllPurchasedCharacter().Count; i++)
        {
            int purchaseCharacterIndex = GameDataManager.GetPurchasedCharacter(i);
            characterDB.PurchaseCharacter(purchaseCharacterIndex);
        }

        itemHeight = ShopItemsContainer.GetChild(0).GetComponent<RectTransform>().sizeDelta.y;
        Destroy(ShopItemsContainer.GetChild(0).gameObject);
        ShopItemsContainer.DetachChildren();

        for (int i = 0; i < characterDB.CharacterCount; i++)
        {
            CharacterData character = characterDB.GetCharacter(i);
            CharacterUiItems uiItem = Instantiate(itemPrefab, ShopItemsContainer).GetComponent<CharacterUiItems>();

            //Move item to its position
            uiItem.SetItemPosition(Vector2.down * i * (itemHeight + itemSpacing));

            //Set item name
            uiItem.gameObject.name = "Item" + i + "-" + character.name;

            //Add information
            uiItem.SetcharacterName(character.name);
            uiItem.SetcharacterImage(character.image);
            uiItem.SetcharacterGems(character.gems);

            if (character.isPurchased)
            {
                //character is Purchased
                uiItem.SetCharacterAsPurchased();
                uiItem.OnItemSelect(i, OnItemSelected);
            }
            else
            {
                // is not Purchased
                uiItem.SetcharacterGems(character.gems);
                uiItem.OnItemPurchase(i, OnItemPurchase);
            }

            //resize item
            ShopItemsContainer.GetComponent<RectTransform>().sizeDelta =
            Vector2.up * ((itemHeight + itemSpacing) * characterDB.CharacterCount + itemSpacing);
        }
    }

    void ChangePlayerSkin()
    {
        CharacterData characterData = GameDataManager.GetSelectedCharacter();
    }

    void OnItemSelected(int index)
    {
        // Debug.Log("select" + index);

        SelectItemUI(index);

        //save data
        GameDataManager.SetSelectedCharacter(characterDB.GetCharacter(index), index);

        //change character
        ChangePlayerSkin();
    }

    void SelectItemUI(int itemIndex)
    {
        priviousSelectedItemIndex = newSelectedItemIndex;
        newSelectedItemIndex = itemIndex;

        CharacterUiItems prevUiItem = GetItemUI(priviousSelectedItemIndex);
        CharacterUiItems newUiItem = GetItemUI(newSelectedItemIndex);

        prevUiItem.DeselectItem();
        newUiItem.SelectItem();
    }

    CharacterUiItems GetItemUI(int index)
    {
        return ShopItemsContainer.GetChild(index).GetComponent<CharacterUiItems>();
    }

    void OnItemPurchase(int index)
    {
        // Debug.Log("Purchased" + index);

        CharacterData characterData = characterDB.GetCharacter(index);
        CharacterUiItems uiItems = GetItemUI(index);

        if (GameDataManager.CanSpendGems(characterData.gems))
        {
            //Proceed with the purchase operation
            GameDataManager.SpendGems(characterData.gems);

            //Play puchase fx

            //Update gems ui text
            GameSharedUI.Instance.UpdateGemsUIText();

            characterDB.PurchaseCharacter(index);

            uiItems.SetCharacterAsPurchased();
            uiItems.OnItemSelect(index, OnItemSelected);

            //Add purchase item to shop data
            GameDataManager.AddPurchasedCharacter(index);
        }
        else
        {
            //Show no enough gems message
            AnimateNoMoreGemsText();
            // Debug.Log("No enough gems!!");
            uiItems.AnimateShakeItem();
        }
    }

    void AnimateNoMoreGemsText()
    {
        //complete animation
        noEnoughGemsText.transform.DOComplete();
        noEnoughGemsText.DOComplete();

        noEnoughGemsText.transform.DOShakePosition(3f, new Vector3(5f, 0f, 0f), 10, 0);
        noEnoughGemsText.DOFade(1f, 3f).From(0f).OnComplete(() =>
        {
            noEnoughGemsText.DOFade(0f, 1f);
        });

    }

    void AddShopEvents()
    {
        openShopButton.onClick.RemoveAllListeners();
        openShopButton.onClick.AddListener(OpenShop);

        closeShopButton.onClick.RemoveAllListeners();
        closeShopButton.onClick.AddListener(CloseShop);
    }

    void OpenShop()
    {
        shopUI.SetActive(true);
    }

    void CloseShop()
    {
        shopUI.SetActive(false);
    }
}
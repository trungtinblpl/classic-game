using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;
using DG.Tweening;

public class CharacterUiItems : MonoBehaviour
{
    [SerializeField] Color itemNotSelectedColor;
    [SerializeField] Color itemSelectedColor;

    [Space(20f)]
    [SerializeField] Image characterImage;
    [SerializeField] TMP_Text characterNameText;
    [SerializeField] TMP_Text characterGemsText;
    [SerializeField] Button characterPurchaseButton;

    [Space(20f)]
    [SerializeField] Button itemsButton;
    [SerializeField] Image itemImage;
    [SerializeField] Outline itemOuline;

    //--------------------------------------
    public void SetItemPosition(Vector2 position)
    {
        GetComponent<RectTransform>().anchoredPosition = position;
    }


    public void SetcharacterImage(Sprite sprite)
    {
        characterImage.sprite = sprite;
    }

    public void SetcharacterName(string name)
    {
        characterNameText.text = name;
    }

    public void SetcharacterGems(int gems)
    {
        characterGemsText.text = gems.ToString();
    }

    public void SetCharacterAsPurchased()
    {
        characterPurchaseButton.gameObject.SetActive(false);
        // to do color
        itemsButton.interactable = true;

        itemImage.color = itemNotSelectedColor;
    }

    public void OnItemPurchase(int itemIndex, UnityAction<int> action)
    {
        characterPurchaseButton.onClick.RemoveAllListeners();
        characterPurchaseButton.onClick.AddListener(() => action.Invoke(itemIndex));
    }

    public void OnItemSelect(int itemIndex, UnityAction<int> action)
    {
        itemsButton.interactable = true;
        itemsButton.onClick.RemoveAllListeners();
        itemsButton.onClick.AddListener(() => action.Invoke(itemIndex));
    }

    public void SelectItem()
    {
        itemOuline.enabled = true;
        itemImage.color = itemSelectedColor;
        itemsButton.interactable = false;
    }

    public void DeselectItem()
    {
        itemOuline.enabled = false;
        itemImage.color = itemNotSelectedColor;
        itemsButton.interactable = true;
    }

    public void AnimateShakeItem()
    {
        //end all animation
        transform.DOComplete();

        transform.DOShakePosition(1f, new Vector3(8f, 0, 0), 10, 0).SetEase(Ease.Linear);
    }
}

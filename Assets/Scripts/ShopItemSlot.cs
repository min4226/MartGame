using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopItemSlot : MonoBehaviour
{
    [SerializeField] Image itemSlotSprite;
    [SerializeField] TextMeshProUGUI itemSlotName;
    [SerializeField] TextMeshProUGUI coinText;
    [SerializeField] TextMeshProUGUI fameText;
    ShopItemData shopData;
    public void SetItemData(ShopItemData shopData)
    {
        this.shopData = shopData;

        itemSlotSprite.sprite = shopData.shopItemSprite;
        itemSlotName.text = shopData.shopItemName.ToString();
        coinText.text = shopData.reward.coin.ToString();
        fameText.text = shopData.reward.fame.ToString();
    }

    public ShopItemData GetItem()
    {
        if (shopData != null)
        {
            Debug.Log($"가구 이름 : {shopData.shopItemName}");
        }

        return shopData;
    }

    public void BuyButton()
    {
        if (ShopInventory.Instance == null)
            return;

        ShopInventory.Instance.SelectItem(shopData);
    }
}

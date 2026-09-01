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
        Debug.Log($"SetItemData로 받은 값 : {shopData}");
        Debug.Log($"SET 슬롯 ID : {GetInstanceID()} / data : {shopData}");

        this.shopData = shopData;

        itemSlotSprite.sprite = shopData.shopItemSprite;
        itemSlotName.text = shopData.shopItemName.ToString();
        coinText.text = shopData.reward.coin.ToString();
        fameText.text = shopData.reward.fame.ToString();
    }

    public ShopItemData GetItem()
    {
        Debug.Log($"GetItem에서 반환하는 shopData : {shopData}");
        Debug.Log($"GET 슬롯 ID : {GetInstanceID()} / data : {shopData}");
        if (shopData != null)
        {
            Debug.Log($"가구 이름 : {shopData.shopItemName}");
        }

        return shopData;
    }

    public void BuyButton()
    {
        if (shopData == null)
        {
            Debug.LogError("shopData가 null입니다.");
            return;
        }

        ShopInventory.Instance.SelectItem(shopData);
    }
}

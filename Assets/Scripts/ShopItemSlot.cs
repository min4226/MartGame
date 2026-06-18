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

    public void SetItemData(ShopItemData shopData)
    {
        itemSlotSprite.sprite = shopData.shopItemSprite;
        itemSlotName.text = shopData.shopItemName.ToString();
        coinText.text = shopData.reward.coin.ToString();
        fameText.text = shopData.reward.fame.ToString();
    }

    internal Item GetItem()
    {
        throw new NotImplementedException();
    }
}

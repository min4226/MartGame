using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
    [SerializeField] GameObject itemSlotSprite;
    [SerializeField] TextMeshProUGUI itemSlotName;
    [SerializeField] TextMeshProUGUI coinText;
    [SerializeField] TextMeshProUGUI fameText;

    public void SetItemData(ItemData itemData)
    {
        itemSlotSprite = itemData.itemSprite;
        itemSlotName.text = itemData.itemName.ToString();
        coinText.text = itemData.reward.coin.ToString();
        fameText.text = itemData.reward.fame.ToString();
    }

    internal Item GetItem()
    {
        throw new NotImplementedException();
    }
}

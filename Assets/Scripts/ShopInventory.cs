using System.Collections.Generic;
using UnityEngine;

public class ShopInventory : MonoBehaviour
{
    public static ShopInventory Instance;

    [SerializeField] ShopData shopData;

    Dictionary<ShopItemData, int> items = new();

    ShopItemData selectedItem;

    private void Awake()
    {
        Instance = this;

        // ShopData가 가지고 있는 모든 ShopItemData를 가져옴
        foreach (ShopItemData item in shopData.items)
        {
            items.Add(item, 0);
        }
    }

    public void SelectItem(ShopItemData item)
    {
        selectedItem = item;

        Debug.Log($"구매 선택 : {item.shopItemName}");

        UIManager.ClaimOpenUI(UIType.PayWindow);
    }

    public void BuySelectedItem()
    {
        if (selectedItem == null)
            return;

        if (items.ContainsKey(selectedItem))
        {
            items[selectedItem]++;
        }
        else
        {
            items.Add(selectedItem, 1);
        }

        Debug.Log(
            $"{selectedItem.shopItemName} 보유 개수 : {items[selectedItem]}"
        );

        selectedItem = null;

        UIManager.ClaimCloseUI(UIType.PayWindow);
    }

    public Dictionary<ShopItemData, int> GetItems()
    {
        return items;
    }
}
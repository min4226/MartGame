using System.Collections.Generic;
using UnityEngine;

public class ShopInventory : MonoBehaviour
{
    public static ShopInventory Instance;

    [SerializeField] ShopData shopData;
    [SerializeField] MyItemInventoryUI myItemInventoryUI;

    Dictionary<ShopItemData, int> items = new();

    ShopItemData selectedItem;

    private void Awake()
    {
        Debug.Log(
            $"ShopInventory Awake / ID : {GetInstanceID()} / " +
            $"Object : {gameObject.name} / " +
            $"Scene : {gameObject.scene.name}"
        );

        Instance = this;

        foreach (ShopItemData item in shopData.items)
        {
            items.Add(item, 0);
        }
    }

    public void SelectItem(ShopItemData item)
    {
        Debug.Log($"SelectItem 실행 / 받은 item : {item}");

        selectedItem = item;

        Debug.Log($"selectedItem에 저장된 값 : {selectedItem}");

        UIManager.ClaimOpenUI(UIType.PayWindow);
    }

    public void BuySelectedItem()
    {
        Debug.Log($"BuySelectedItem 실행 / selectedItem : {selectedItem}");

        if (selectedItem == null)
        {
            Debug.LogError("selectedItem이 null입니다!");
            return;
        }

        items[selectedItem]++;

        Debug.Log(
            $"{selectedItem.shopItemName} 보유 개수 : {items[selectedItem]}"
        );

        myItemInventoryUI.Refresh();

        selectedItem = null;

        UIManager.ClaimCloseUI(UIType.PayWindow);
    }

    public Dictionary<ShopItemData, int> GetItems()
    {
        return items;
    }
}
using System.Collections.Generic;
using UnityEngine;

public class ShopInventory : MonoBehaviour
{
    public static ShopInventory Instance;

    [SerializeField] ShopData shopData;
    ItemlListInstance itemlistinstance;

    Dictionary<ShopItemData, int> items = new();

    ShopItemData selectedItem;

    private void Awake()
    {
        itemlistinstance = GetComponent<ItemlListInstance>();
        Instance = this;

        foreach (ShopItemData item in shopData.items)
        {
            items.Add(item, 0);
        }
    }

    public void SelectItem(ShopItemData item)
    {
        selectedItem = item;
        UIManager.ClaimOpenUI(UIType.PayWindow);
    }

    public void BuySelectedItem()
    { 
        items[selectedItem]++;

        Debug.Log(
            $"{selectedItem.shopItemName} 보유 개수 : {items[selectedItem]}"
        );

        MyItemInventoryUI ui = FindFirstObjectByType<MyItemInventoryUI>(FindObjectsInactive.Include);


        Debug.Log($"구매 후 MyItemInventoryUI : {ui}");

        if (ui != null)
        {
            ui.Refresh();
        }

        selectedItem = null;

        UIManager.ClaimCloseUI(UIType.PayWindow);
    }

    public Dictionary<ShopItemData, int> GetItems()
    {
        return items;
    }
}
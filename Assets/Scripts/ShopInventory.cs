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
        Debug.Log(
            $"ShopInventory Awake / ID : {GetInstanceID()} / " +
            $"Object : {gameObject.name} / " +
            $"Scene : {gameObject.scene.name}"
        );
        itemlistinstance = GetComponent<ItemlListInstance>();
        Instance = this;

        foreach (ShopItemData item in shopData.items)
        {
            items.Add(item, 0);
        }

        
    }

    public void SelectItem(ShopItemData item)
    {
        Debug.Log($"[Select] Inventory ID : {GetInstanceID()}");
        Debug.Log($"[Select] 받은 item : {item}");
        Debug.Log($"[Select] 받은 item 이름 : {(item != null ? item.shopItemName : "NULL")}");

        selectedItem = item;

        Debug.Log($"[Select] selectedItem 저장 후 : {selectedItem}");

        UIManager.ClaimOpenUI(UIType.PayWindow);
    }

    public void BuySelectedItem()
    {
        if (selectedItem == null)
        {
            Debug.LogError("selectedItem이 null입니다!");
            return;
        }

        items[selectedItem]++;

        Debug.Log(
            $"{selectedItem.shopItemName} 보유 개수 : {items[selectedItem]}"
        );

        MyItemInventoryUI ui =
            FindFirstObjectByType<MyItemInventoryUI>(FindObjectsInactive.Include);

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
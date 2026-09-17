using System.Collections.Generic;
using UnityEngine;

public class ShopInventory : MonoBehaviour
{
    public static ShopInventory Instance;

    [SerializeField] ShopData shopData;
    [SerializeField] ShopItemPay shopItemPay;
    ItemlListInstance itemlistinstance;

    Dictionary<ShopItemData, int> items = new();
    PayCountChange payCountChange;
    ShopItemData selectedItem;
    [SerializeField] UserPayCount userPayCount;
    private void Awake()
    {
        itemlistinstance = GetComponent<ItemlListInstance>();
        payCountChange = GetComponent<PayCountChange>();
        Debug.Log($"paycountchange : {payCountChange}");
        Instance = this;
        
        foreach (ShopItemData item in shopData.items)
        {
            items.Add(item, 0);
        }
    }

    public void SelectItem(ShopItemData item)
    {
        selectedItem = item;
        //userPayCount.ResetTextCount();
        payCountChange.Init(1);
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
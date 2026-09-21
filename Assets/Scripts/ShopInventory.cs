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
    UserPayCount userPayCount;
    private void Awake()
    {
        itemlistinstance = GetComponent<ItemlListInstance>();
        Canvas canvas = FindFirstObjectByType<Canvas>();

        userPayCount = FindFirstObjectByType<UserPayCount>(FindObjectsInactive.Include);

        
        GameObject countCountText = userPayCount.transform.Find("CountCountText").gameObject;
        
        payCountChange = countCountText.GetComponent<PayCountChange>();
        
        Instance = this;
        
        foreach (ShopItemData item in shopData.items)
        {
            items.Add(item, 0);
        }
    }

    public void SelectItem(ShopItemData item)
    {
        selectedItem = item;
        payCountChange.Init(1);
        UIManager.ClaimOpenUI(UIType.PayWindow);
    }

    public void BuySelectedItem()
    {
        if (selectedItem == null)
        {
            return;
        }

        items[selectedItem]++;

        MyItemInventoryUI ui = FindFirstObjectByType<MyItemInventoryUI>(FindObjectsInactive.Include);


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
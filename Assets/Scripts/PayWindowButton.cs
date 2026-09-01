using UnityEngine;

public class PayWindowButton : MonoBehaviour
{
    [SerializeField] ShopItemSlot shopItemSlot;

    public void BuyShopItem()
    {
        ShopItemData item = shopItemSlot.GetItem();
        Debug.Log($"buyshopitem item : {item}");
        Debug.Log($"shopitemslot : {shopItemSlot}");
        ShopInventory.Instance.SelectItem(item);
    }
}
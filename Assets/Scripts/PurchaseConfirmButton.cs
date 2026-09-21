using UnityEngine;

public class PurchaseConfirmButton : MonoBehaviour
{
    public void ConfirmPurchase()
    {
        if (ShopInventory.Instance == null)
            return;

        ShopInventory.Instance.BuySelectedItem();
    }
}
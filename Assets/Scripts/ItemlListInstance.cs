using UnityEngine;

public class ItemlListInstance : MonoBehaviour
{
    [SerializeField] GameObject shopItem;
    [SerializeField] GameObject myItem;
    MyItemInventoryUI myItemInventoryUI;
    public MyItemInventoryUI MyItemInventoryUI => myItemInventoryUI;
    public void OnShopItemListButton()
    {
        shopItem.SetActive(true);
        myItem.SetActive(false);
    }

    public void OnMyItemListButton()
    {
        myItem.SetActive(true);
        myItemInventoryUI = myItem.GetComponentInChildren<MyItemInventoryUI>(true);
        Debug.Log($"myItem : {myItem}");
        Debug.Log($"myItemInventoryUI : {myItemInventoryUI}");
        shopItem.SetActive(false);
    }

    public void OnBothExitButton()
    {
        myItem.SetActive(false);
        shopItem.SetActive(false);
    }

}

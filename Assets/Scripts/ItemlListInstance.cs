using UnityEngine;

public class ItemlListInstance : MonoBehaviour
{
    [SerializeField] GameObject shopItem;
    [SerializeField] GameObject myItem;

    void Awake()
    {
        shopItem.SetActive(false);
        myItem.SetActive(false);
    }
    public void OnItemList()
    {
        shopItem.SetActive(true);
        myItem.SetActive(true);
    }

    public void OnShopItemListButton()
    {
        if (shopItem.CompareTag("ShopItemListButton"))
        {
            shopItem.SetActive(true);
            myItem.SetActive(false);
        }
    }

    public void OnMyItemListButton()
    {
        if (myItem.CompareTag("MyItemListButton"))
        {
            myItem.SetActive(true);
            shopItem.SetActive(false);
        }
    }
 
}

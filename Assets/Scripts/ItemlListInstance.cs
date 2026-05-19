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

    public void OnShopItemListButton()
    {
        shopItem.SetActive(true);
        myItem.SetActive(false);
    }

    public void OnMyItemListButton()
    {
        myItem.SetActive(true);
        shopItem.SetActive(false);
    }
 
}

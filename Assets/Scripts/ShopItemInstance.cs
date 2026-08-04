using UnityEngine;

public class ShopItemInstance : MonoBehaviour
{
    public ShopData shopData; 
    public GameObject itemSlotPrefab; 
    public Transform content;

    void Start()
    {

        for (int i = 0; i < shopData.items.Length; i++)
        {
            GameObject itemSlotObject = Instantiate(itemSlotPrefab, content);
            ShopItemSlot slot = itemSlotObject.GetComponent<ShopItemSlot>();
            Debug.Log(itemSlotObject);
            Debug.Log(slot);
            slot.SetItemData(shopData.items[i]);
        } 
        
    }
}
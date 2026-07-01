using UnityEngine;

public class ShopItemInstance : MonoBehaviour
{
    public ShopData shopData; // 상점템들의 아이템이 들어가 있는 scriptable object
    public GameObject itemSlotPrefab; // 아이템 슬롯 창
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
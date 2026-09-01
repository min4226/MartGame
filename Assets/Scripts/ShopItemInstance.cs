using UnityEngine;

public class ShopItemInstance : MonoBehaviour
{
    public ShopData shopData; 
    public GameObject itemSlotPrefab; 
    public Transform content;

    void Start()
    {
        Debug.Log("ShopItemInstance Start 실행");

        for (int i = 0; i < shopData.items.Length; i++)
        {
            Debug.Log($"items[{i}] = {shopData.items[i]}");

            GameObject itemSlotObject = Instantiate(itemSlotPrefab, content);

            ShopItemSlot slot = itemSlotObject.GetComponent<ShopItemSlot>();

            Debug.Log($"생성된 slot = {slot}");

            slot.SetItemData(shopData.items[i]);
        }
    }
}
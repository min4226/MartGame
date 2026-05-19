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
            GameObject itemSlotObj = Instantiate(itemSlotPrefab, content);

            ItemSlot slot = itemSlotObj.GetComponent<ItemSlot>();
            slot.SetItemData(shopData.items[i]);

            Debug.Log("»ý¼ºµÊ: " + shopData.items[i].name);
        }
    }
}
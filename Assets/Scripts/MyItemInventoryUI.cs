using UnityEngine;

public class MyItemInventoryUI : MonoBehaviour
{
    [SerializeField] GameObject itemSlotPrefab;
    [SerializeField] Transform content;

    public void Refresh()
    {
        Debug.Log("Refresh 실행!");

        // 기존 슬롯 삭제
        foreach (Transform child in content)
        {
            Destroy(child.gameObject);
        }

        foreach (var item in ShopInventory.Instance.GetItems())
        {
            Debug.Log($"{item.Key.shopItemName} : {item.Value}");

            if (item.Value <= 0)
                continue;

            GameObject slotObject = Instantiate(itemSlotPrefab, content);

            MyItemSlot slot = slotObject.GetComponent<MyItemSlot>();

            slot.SetItem(item.Key, item.Value);
        }
    }
}
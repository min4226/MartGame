using UnityEngine;

public class MyItemInventoryUI : MonoBehaviour
{
    [SerializeField] GameObject itemSlotPrefab;
    [SerializeField] Transform content;

    public void Refresh()
    {
        // 기존 슬롯 삭제
        foreach (Transform child in content)
        {
            Destroy(child.gameObject);
        }

        // 보유 아이템 생성
        foreach (var item in ShopInventory.Instance.GetItems())
        {
            GameObject slotObject =
                Instantiate(itemSlotPrefab, content);

            MyItemSlot slot =
                slotObject.GetComponent<MyItemSlot>();

            slot.SetItem(item.Key, item.Value);
        }
    }
}
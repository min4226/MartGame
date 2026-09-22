using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MyItemSlot : MonoBehaviour
{
    [SerializeField] private Image itemImage;
    [SerializeField] private TextMeshProUGUI itemName;
    [SerializeField] private TextMeshProUGUI countText;

    private ShopItemData itemData;
    private int itemCount;

    public void SetItem(ShopItemData data, int count)
    {
        Debug.Log("set item 함수 실행");

        itemData = data;
        itemCount = count;

        itemImage.sprite = data.shopItemSprite;
        itemName.text = data.shopItemName;
        countText.text = $"x{itemCount}";
    }

    public void OnClickSlot()
    {
        // 보유 수량이 없으면 배치하지 않음
        if (itemCount <= 0)
        {
            Debug.Log("보유 수량이 없습니다.");
            return;
        }

        FurnitureDrag drag =
            FindFirstObjectByType<FurnitureDrag>(
                FindObjectsInactive.Include
            );

        if (drag == null)
        {
            Debug.LogError("FurnitureDrag를 찾을 수 없습니다.");
            return;
        }

        drag.StartDrag(itemData, this);
    }

    // 가구를 실제로 배치했을 때 호출
    // 여기를 고쳐야함 사용을 했을 때 itemcount를 다시 담아야할 것 같음
    public void UseItem()
    {
        if (itemCount <= 0)
            return;

        itemCount--;

        var items = ShopInventory.Instance.GetItems();

        if (items.ContainsKey(itemData))
        {
            items[itemData]--;
        }

        countText.text = $"x{itemCount}";

        Debug.Log("가구 1개 사용! 남은 수량: " + itemCount);
    }
}
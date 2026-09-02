using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MyItemSlot : MonoBehaviour
{
    [SerializeField] Image itemImage;
    [SerializeField] TextMeshProUGUI itemName;
    [SerializeField] TextMeshProUGUI countText;

    public void SetItem(ShopItemData data, int count)
    {
        itemImage.sprite = data.shopItemSprite;
        itemName.text = data.shopItemName.ToString();
        countText.text = $"x{count}";
    }
}
using UnityEngine;
using UnityEngine.UI;


public class ItemListUI : MonoBehaviour
{
    [SerializeField] Image itemImage;
    [SerializeField] TMPro.TextMeshProUGUI itemName;
    [SerializeField] TMPro.TextMeshProUGUI itemPrice;
    public void set(ItemData itemData)
    {
        itemName.text = itemData.itemName.ToString();
        itemPrice.text = itemData.itemBasePrice.ToString();
        itemImage.sprite = itemData.itemSprite;
        Debug.Log($"itemimage : {itemImage}");
    }
}

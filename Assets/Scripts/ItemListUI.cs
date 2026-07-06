using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class ItemListUI : MonoBehaviour
{
    [SerializeField] GameObject itemImage;
    [SerializeField] TMPro.TextMeshProUGUI itemName;
    [SerializeField] TMPro.TextMeshProUGUI itemPrice;
    public void set(ItemData itemData)
    {
        Debug.Log("set ½ÇÇà Áß");
        itemName.text = itemData.itemName.ToString();
        itemPrice.text = itemData.itemBasePrice.ToString();
        itemImage = itemData.itemSprite;
        Debug.Log($"itemname : {itemName == null}");
    }
}

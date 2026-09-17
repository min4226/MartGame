using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopItemPay : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI shopItemPayCount;
    
    private int shopItemCount;
    private void Start()
    {
        shopItemPayCount.text = shopItemCount.ToString();
    }
    public void Init()
    {
        shopItemCount = 1;
        shopItemPayCount.text = shopItemCount.ToString();
    }
}

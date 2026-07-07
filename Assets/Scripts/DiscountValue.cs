  using UnityEngine;


public class DiscountValue : MonoBehaviour
{
    ItemData itemData;
    public int[] discountRates = {10, 20, 30, 40, 50 };
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

   
    // 할인율 계산
    public void ItemDiscount(ItemData itemData, int percent)
    { 
        percent = discountRates[Random.Range(0, discountRates.Length)];
        itemData.itemBasePrice = Mathf.RoundToInt(itemData.itemBasePrice * (100 - percent) / 100);
    }

    public void ItemChange(ItemData newItem)
    { 
        
        
    }
}

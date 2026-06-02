using System.Collections;
using UnityEngine;

public class NormalCustomer : MonoBehaviour
{
    
    [SerializeField] NormalCustomerItem[] items;
    [SerializeField] Transform itemPool;
    
    void Start()
    {
        StartCoroutine(ItemCreate());
    }

    IEnumerator ItemCreate()
    {
        

        while (true)
        {
            NormalCustomerItem customerItem = items[Random.Range(0, items.Length)];
            ItemData itemData = customerItem.item[Random.Range(0, customerItem.item.Length)];

            GameObject normalItem  = Instantiate(itemData.itemSprite, itemPool, false);
            yield return new WaitForSeconds(2);

        }

    }

    
}

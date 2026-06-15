using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class NormalCustomer : MonoBehaviour
{
    
    [SerializeField] NormalCustomerItem[] items;
    [SerializeField] Transform itemPool;
    int speed = 3;
    void Start()
    {
        
        StartCoroutine(ItemCreate());
    }

    IEnumerator ItemCreate(int count)
    {
        for (int i = 0; i < count; i++)
        {
            if (items.Length == 0) yield break;

            NormalCustomerItem customerItem = items[Random.Range(0, items.Length)];

            if (customerItem.item.Length == 0) continue;

            ItemData itemData = customerItem.item[Random.Range(0, customerItem.item.Length)];

            GameObject normalItem = Instantiate(itemData.itemSprite, itemPool, false);

            var move = normalItem.GetComponent<MoveRight>();
            if (move == null)
                normalItem.AddComponent<MoveRight>();

            yield return new WaitForSeconds(2);
        }
    }


}

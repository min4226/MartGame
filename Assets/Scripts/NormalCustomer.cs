using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

public class NormalCustomer : MonoBehaviour
{
    [SerializeField] NormalCustomerItem[] items;
    [SerializeField] Transform itemPool;
    [SerializeField] StageContainer stageContainer;
    

    

    GameObject normalItem;
    int normalItemCount;
    int speed = 3;
    int currentIndex;
    

    public void Init(StageContainer data)
    {
        
        stageContainer = data;
        StartCoroutine(ItemCreate());
    }

    IEnumerator ItemCreate()
    {

        int count = stageContainer.stageDatas[currentIndex].normalCustomerItemCount; 
        
        for (int i = 0; i < count; i++)
        {
            if (items.Length == 0) yield break;

            NormalCustomerItem customerItem = items[Random.Range(0, items.Length)];

            if (customerItem.item.Length == 0) continue;

            ItemData itemData = customerItem.item[Random.Range(0, customerItem.item.Length)];

            normalItem = Instantiate(itemData.itemSprite, itemPool, false);

            if (!normalItem.TryGetComponent<MoveRight>(out var move))
                move = normalItem.AddComponent<MoveRight>();

            move.speed = speed; 

            yield return new WaitForSeconds(2f);
        }
    }

   

}
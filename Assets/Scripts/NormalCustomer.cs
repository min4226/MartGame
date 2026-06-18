using System.Collections;
using UnityEngine;

public class NormalCustomer : MonoBehaviour
{
    [SerializeField] NormalCustomerItem[] items;
    [SerializeField] Transform itemPool;
    [SerializeField] StageContainer stageContainer;

    int speed = 3;
    int currentIndex;
    public void Init(StageContainer data)
    {
        stageContainer = data;
        StartCoroutine(ItemCreate());
    }

    IEnumerator ItemCreate()
    {

        int count = stageContainer.stageDatas[currentIndex].normalCustomerItemCount; // 여기가 문제
        Debug.Log($"count 생성 개수 : {count}");
        for (int i = 0; i < count; i++)
        {
            Debug.Log($"Instantiate 실행 {i}");
            if (items.Length == 0) yield break;

            var customerItem = items[Random.Range(0, items.Length)];

            if (customerItem.item.Length == 0) continue;

            var itemData = customerItem.item[Random.Range(0, customerItem.item.Length)];

            GameObject normalItem = Instantiate(itemData.itemSprite, itemPool, false);

            if (!normalItem.TryGetComponent<MoveRight>(out var move))
                move = normalItem.AddComponent<MoveRight>();

            move.speed = speed; 

            yield return new WaitForSeconds(2f);
        }
    }
}
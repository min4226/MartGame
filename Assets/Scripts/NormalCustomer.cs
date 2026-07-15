using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

public class NormalCustomer : MonoBehaviour
{
    [SerializeField] NormalCustomerItem[] items;
    [SerializeField] Transform itemPool;
    [SerializeField] StageContainer stageContainer;
    [SerializeField] Trigger trigger;
    [SerializeField] NormalCustomerItem todayItem;
    
    public List<ItemData> todayItems = new List<ItemData>();

    GameObject normalItem;
    int speed = 3;
    int currentIndex;
    

    public void Init(StageContainer data)
    {
        if (GameManager.Instance.CurrentState != GameState.PlayScene)
            return;
        stageContainer = data;
        
    }

    public IEnumerator ItemCreate()
    {
        if (GameManager.Instance.currentCustomer == null) yield break; // ienumerator에서는 return과 같은 뜻으로 yield break를 사용
        todayItems.Clear();
        yield return new WaitForSeconds(2f);
        int count = stageContainer.stageDatas[GameManager.Instance.Stage.CurrentIndex].normalCustomerItemCount;
        trigger.SetItemCount(count);
        
        for (int i = 0; i < count; i++)
        {
            if (items.Length == 0) yield break;

            NormalCustomerItem customerItem = items[Random.Range(0, items.Length)];
            
            if (customerItem.item.Length == 0) continue;

            ItemData itemData = customerItem.item[Random.Range(0, customerItem.item.Length)];
            todayItems.Add(itemData);
            
            normalItem = Instantiate(itemData.itemPrefab, itemPool, false);
            
            

            if (!normalItem.TryGetComponent<MoveRight>(out var move))
                move = normalItem.AddComponent<MoveRight>();

            move.speed = speed;

            yield return new WaitForSeconds(2f);
        }
    }

    public int ItemTotalValue(List<ItemData> todayItems)
    {
        int total = 0;
        foreach (ItemData currentItem in todayItems)
        {
            total += currentItem.itemBasePrice;
        }
        return total;
    }

}
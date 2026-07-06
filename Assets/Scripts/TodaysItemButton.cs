using System.Collections.Generic;
using UnityEngine;

public class TodaysItemButton : MonoBehaviour
{
    [SerializeField] GameObject todaysItemPanel;
    [SerializeField] Transform content;
    
    
    void OnEnable()
    {
        
        foreach (ItemData itemData in GameManager.Instance.NormalCustomer.todayItems)
        {
            Debug.Log($"normal.todayitems : {GameManager.Instance.NormalCustomer.todayItems == null}");
            Debug.Log($"item : {GameManager.Instance.NormalCustomer.todayItems.Count}");
            GameObject ui = Instantiate(todaysItemPanel, content);
            
            ItemListUI itemListUI  = ui.GetComponent<ItemListUI>();
            Debug.Log($"itemListui : {itemListUI}");
            itemListUI.set(itemData);
            Debug.Log($"set실행 중이다.");
        }
    }
}

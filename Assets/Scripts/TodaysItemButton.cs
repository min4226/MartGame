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
            GameObject ui = Instantiate(todaysItemPanel, content);
            ItemListUI itemListUI  = ui.GetComponent<ItemListUI>();
            itemListUI.set(itemData);
            
        }
    }
}

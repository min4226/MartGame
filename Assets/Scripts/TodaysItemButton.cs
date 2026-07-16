using System.Collections.Generic;
using UnityEngine;

public class TodaysItemButton : MonoBehaviour
{
    [SerializeField] GameObject todaysItemPanel;
    [SerializeField] Transform content;
    NormalCustomer normalCustomer;



    void OnEnable()
    {
        RegistCustomer(GameManager.Instance.NormalCustomer);

    }

    private void OnDisable()
    {
        if(normalCustomer) normalCustomer.OnChangedTodayItems -= ChangedTodayItems;

    }

    void RegistCustomer(NormalCustomer newNormalCustomer)
    {        
        if(normalCustomer) normalCustomer.OnChangedTodayItems -= ChangedTodayItems;

        ChangedTodayItems(newNormalCustomer.todayItems);

        newNormalCustomer.OnChangedTodayItems -= ChangedTodayItems;
        newNormalCustomer.OnChangedTodayItems += ChangedTodayItems;

        normalCustomer = newNormalCustomer;
    }

    public void ChangedTodayItems(List<ItemData> todayItems)
    {
        foreach (Transform currentChild in content)
        { 
            Destroy(currentChild.gameObject, 0.01f);
        }

        foreach (ItemData itemData in todayItems)
        {
            
            GameObject ui = Instantiate(todaysItemPanel, content);
            ItemListUI itemListUI  = ui.GetComponent<ItemListUI>();
            itemListUI.set(itemData);
            
        }
    }
}

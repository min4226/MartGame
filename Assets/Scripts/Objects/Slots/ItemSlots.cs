using System;
using Unity.VisualScripting;
using UnityEngine;

public class ItemSlots
{
    [SerializeField] item item;
    [SerializeField] int currentStack;

    public virtual bool Containable(item newItem)          
    {
        if (item) return true;
        else return false;
    }

    public item GetItem() => item;
    public int GetStack() => currentStack;
    public bool GetIsMax() => item ? currentStack >= item.maxStack : false;

    public int AddItem(item wantItem, int amount)
    {
        if (wantItem is null) return 0;
        if (amount <= 0) return 0;
        if (item is not null && item != wantItem) return amount;

        return amount;
    }
}

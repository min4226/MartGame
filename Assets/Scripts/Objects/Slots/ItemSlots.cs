using System;
using Unity.VisualScripting;
using UnityEngine;

public delegate void ItemSlotChangeEvent(ItemSlots changedSlot);
public class ItemSlots

{
    [SerializeField] Item item;
    [SerializeField] int currentStack;
    public event ItemSlotChangeEvent OnItemSlotChanged;
    public void NoticeChanged() => OnItemSlotChanged?.Invoke(this);
    public virtual bool Containable(Item wantItem)          
    {
        if (!wantItem) return false;
        
        if (item && item != wantItem) return false;
        if(GetIsMax()) return false;

        return true;
        
    }

    public Item GetItem() => item;
    public int GetStack() => currentStack;
    public bool GetIsMax() => item ? currentStack >= item.maxStack : false;

    public bool GetIsEmpty() => item is null || currentStack <= 0;
    public int AddItem(Item wantItem, int amount)
    {

        if (amount <= 0) return 0;
        if (!Containable(wantItem)) return amount;
            item = wantItem;
        int stackable = Mathf.Min(item.maxStack - currentStack, amount);
        currentStack += stackable;

        return amount - stackable; // 추가하고싶은 값 - 추가한 값
    }

    public int Clear()
    {
        item = null;
        int removed = currentStack;
        currentStack = 0;
        return removed;
    }

    public int RemoveItem(Item wantItem)
    {
        if (!wantItem) return 0;
        if(GetIsEmpty()) return 0;
        if (item != wantItem) return 0;

        return Clear();
        
    }

    public int RemoveItem(Item wantItem, int amount)
    {
        if (amount <= 0) return 0;
        if (!wantItem) return 0;
        if (GetIsEmpty()) return amount;
        if (item != wantItem) return amount;
        if (amount >= currentStack)
        {
            return amount - Clear();
        }
        currentStack -= amount;
        return 0;
    }
    
}

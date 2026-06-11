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
    public int GetStackable(Item wantItem) => Containable(wantItem) ? wantItem.maxStack - currentStack : 0;
    public int GetStackable() => GetStackable(item);
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
    public int GiveItem(ItemSlots wantSlot) => GiveItem(wantSlot, currentStack);
    public int GiveItem(ItemSlots wantSlot, int amount)
    {
        if (wantSlot is null) return amount;
        if (!item) return amount;
        if (currentStack <= 0 || amount <= 0) return amount;
        Item targetItem = item;
        amount = Mathf.Min(amount, wantSlot.GetStackable(targetItem));
        amount -= RemoveItem(targetItem, amount);
        amount = wantSlot.AddItem(targetItem, amount);

        return amount;
    }

    public void ExchangeItem(ItemSlots wantSlot)
    {
        if (wantSlot is null) return;
        Item wasItem = item ;
        int wasStack = currentStack;
        item = wantSlot.item;
        currentStack = wantSlot.currentStack;
        wantSlot.item = wasItem;
        wantSlot.currentStack = wasStack;
    }

    public void LeftClick(ItemSlots wantSlot)
    {
         // 반올림 :  + 0.5
        if (wantSlot is null) return;
        if (InputManager.IsShift)
        {
            if (wantSlot.GetIsEmpty())
            {
                if (GetIsEmpty()) return;
                else if (wantSlot.Containable(item))
                {
                    GiveItem(wantSlot, Mathf.CeilToInt(currentStack * 0.5f));
                    
                    
                }

            }
            else if (Containable(wantSlot.item))
            {
                
                wantSlot.GiveItem(this, Mathf.CeilToInt(wantSlot.currentStack * 0.5f));
            }

        }
        else
        {
            if (wantSlot.Containable(item))
            {
                GiveItem(wantSlot);
            }
            else
            {
                ExchangeItem(wantSlot);
            }

        }
        
        NoticeChanged();
        wantSlot.NoticeChanged();
    }
}

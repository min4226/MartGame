using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.UIElements;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static ItemSlots cursorSlot = new ItemSlots();
    public int columns;
    public int rows;

    ItemSlots[,] slots;

    public void Initialize()
    {
        slots = new ItemSlots[rows, columns];
        for (int row = 0; row < rows; row++)
        {
            for (int column = 0; column < columns; column++)
            {
                slots[row, column] = new ItemSlots();
            }
        }
    }

    public void HealPotionPlus(int amount)
    {
        Item potion = DataManager.LoadDataFile<Item>("Potion");
        AddItem(potion, amount);
    }

    public void RemovePotionButton(int amount)
    {
        Item potion = DataManager.LoadDataFile<Item>("Potion");
        RemoveItem(potion, amount);
    }

    public void Sort(System.Comparison<Item> Method)
    { 
       
    }

    public void AutoQuickInsert(Inventory other)
    { 
        
    }

    public void AutoQuickInsert(Inventory[] other)
    {

    }

    public bool InsertAll(Inventory other)
    {
        return default;
    }

    public bool InsertAll(Inventory other, Item target)
    {
        return default;
    }

    public void LockSlot(int wantRow, int wantColumn)
    { 
        
    }

    public void UnlockSlot(int wantRow, int wantColumn)
    { 
        
    }

    public int CountItem(Item wantItem)
    {
        return default;
    }

    public int CountItem(Item wantItem, out List<ItemSlot> returnSlots)
    {
        returnSlots = default;
        return default;
    }

    // 원하는 자료형을 반복적으로 내보냄. itemslot을 요구할 때마다 다음 슬롯을 내놓음
    public IEnumerable<ItemSlots> GetAllSlot()
    {
        // ItemSlots[] result = new ItemSlots[slots.Length];

        int height = slots.GetLength(0);
        int width = slots.GetLength(1);
        for (int row = 0; row < height; row++)
        {
            for (int column = 0; column < width; column++)
            {
                if (slots[row, column] is null) continue;
                // 결과를 내보내고 기다림
                yield return slots[row, column];
            }
        }
    }

    public IEnumerable<ItemSlots> GetAllSlotReverse()
    {
        int height = slots.GetLength(0);
        int width = slots.GetLength(1);
        for (int row = height -1; row >= 0; row--)
        {
            for (int column = width -1 ; column >= 0; column--)
            {
                if (slots[row, column] is null) continue;
                yield return slots[row, column];
            }
        }
    }

    public ItemSlots FindItem(Item target)
    {
        return default;
    }

    public ItemSlots FindItem(ItemType wantType)
    {
        return default;
    }

    public ItemSlots FindItem(string containWord)
    {
        return default;
    }

    public ItemSlots FindItem(int wantRow, int wantColumn)
    {
        if (wantRow < 0 || wantColumn < 0) return null;
        if(wantRow >= slots.GetLength(0)) return null;
        if (wantColumn >= slots.GetLength(1)) return null;
        return slots[wantRow, wantColumn];
    }

    public IEnumerable<ItemSlots> FindFirstItem(Item target)
    {
        foreach (ItemSlots currentSlot in GetAllSlot())
        {
            if (currentSlot.GetItem() == target) yield return currentSlot;

        }
    }

    public IEnumerable<ItemSlots> FindLastItem(Item target)
    {
        foreach (ItemSlots currentSlot in GetAllSlotReverse())
        {
            if (currentSlot.GetItem() == target) yield return currentSlot;
        }
        
    }

    public IEnumerable<ItemSlots> FindFirstEmptySlot()
    {
        foreach (ItemSlots currentSlot in GetAllSlot())
        {
            if (currentSlot.GetIsEmpty()) yield return currentSlot;

        }
    }

    public IEnumerable <ItemSlots> FindLastEmptySlot()
    {
        foreach (ItemSlots currentSlot in GetAllSlotReverse())
        {
            if (currentSlot.GetIsEmpty()) yield return currentSlot;
        }
    }

    public int AddItem(Item wantItem, int amount = 1)
    {
        
        amount = AddItemOnExistSlots(wantItem, amount);
        if (amount <= 0) return 0;
        return AddItemOnEmptySlots(wantItem, amount);
    }

    public void ExchangeItem(int startRow, int startColumn, ItemSlots targetSlot)
    {
        if (targetSlot is null) return;
        ItemSlots first = FindItem(startRow, startColumn);
        if (first is null) return;
        
        first.ExchangeItem(targetSlot);
        first.NoticeChanged();
        targetSlot.NoticeChanged();
    }

    public int AddItemOnExistSlots(Item wantItem, int amount)
    {
        foreach (ItemSlots currentSlot in FindFirstItem(wantItem))
        {
            if (amount <= 0) return 0;
            amount = currentSlot.AddItem(wantItem, amount);
            currentSlot.NoticeChanged();
        }
        return amount;
    }

    public int AddItemOnEmptySlots(Item wantItem, int amount)
    {
        foreach (ItemSlots currentSlot in FindFirstEmptySlot())
        {
            if (amount <= 0) return 0;
            amount = currentSlot.AddItem(wantItem, amount);
            currentSlot.NoticeChanged();
        }
        return amount;
    }

    public int AddItemToLocation(Item wantItem, int amount, int row, int column)
    {
        return default;
    }

    public ItemSlots[,] Clear()
    {
        ItemSlots[,] origin = slots;
        Initialize();
        return origin;
    }

    public int RemoveItem(System.Predicate<Item> condition)
    {
        return default;
    }

    public int RemoveItem(Item wantItem)
    {
        int result = 0;
        foreach (ItemSlots currentSlot in FindLastItem(wantItem))
        {
            result += currentSlot.RemoveItem(wantItem);
            currentSlot.NoticeChanged();
        }
        return result
            ;
    }

    public int RemoveItem(Item wantItem, int amount)
    {
        foreach (ItemSlots currentSlot in FindLastItem(wantItem))
        {
            amount = currentSlot.RemoveItem(wantItem, amount);
            currentSlot.NoticeChanged();
        }
        return amount;
    }

    

    public int RemoveItemOnExistSlots(Item wantItem, int amount)
    {
        return default;
        
    }

    
    public int RemoveItemFromLocation(int row, int column)
    {
        return default;
    }

    public int RemoveItemFromLocation(int row, int column , int amount)
    {
        return default;
    }

    // -1 = all
    public void MoveItem(int startRow, int startColumn, Inventory targetInventory, int targetRow, int targetColumn, int amount = -1)
    {

    }
    public void ExchangeItem(int startRow, int startColumn, int targetRow, int targetColumn)
    {
        ExchangeItem(startRow, startColumn, this, targetRow, targetColumn);
    }
    public void ExchangeItem(int startRow, int startColumn, Inventory targetInventory, int targetRow, int targetColumn)
    {
        ItemSlots first = FindItem(startRow, startColumn);
        if (first is null) return;
        if (!targetInventory) return;
        ItemSlots second = targetInventory.FindItem(targetRow, targetColumn);
        if (second is null) return;
        first.ExchangeItem(second);
        first.NoticeChanged();
        second.NoticeChanged();
    }
    
   

    public void UseItem(Item target)
    {

    }

    public void UseItem(int startRow, int startColumn)
    { 
        
    }
}

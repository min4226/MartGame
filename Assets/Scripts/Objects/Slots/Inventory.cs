using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public int columns;
    public int rows;

    ItemSlot[,] slots;

    public void Initialize()
    {
        slots = new ItemSlot[rows, columns];
    }

    public void Sort(System.Comparison<item> Method)
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

    public bool InsertAll(Inventory other, item target)
    {
        return default;
    }

    public void LockSlot(int wantRow, int wantColumn)
    { 
        
    }

    public void UnlockSlot(int wantRow, int wantColumn)
    { 
        
    }

    public int CountItem(item wantItem)
    {
        return default;
    }

    public int CountItem(item wantItem, out List<ItemSlot> returnSlots)
    {
        returnSlots = default;
        return default;
    }

    public ItemSlot FindItem(item target)
    {
        return default;
    }

    public ItemSlot FindItem(ItemType wantType)
    {
        return default;
    }

    public ItemSlot FindItem(string containWord)
    {
        return default;
    }

    public ItemSlot FindItem(int wantRow, int wantColumn)
    {
        return default;
    }

    public ItemSlot FindFirstItem(item target)
    {
        return default;
    }

    public ItemSlot FindLastItem(item target)
    {
        return default;
    }

    public ItemSlot FindFirstEmptySlot()
    {
        return default;
    }

    public ItemSlot FindLastEmptySlot()
    {
        return default;
    }

    public int AddItem(item wantItem, int amount = 1)
    {
        return default;
    }

    public int AddItemOnExistSlots(item wantItem, int amount)
    {
        return default;
    }

    public int AddItemOnEmptySlots(item wantItem, int amount)
    {
        return default;
    }

    public int AddItemToLocation(item wantItem, int amount, int row, int column)
    {
        return default;
    }

    public ItemSlot[,] Clear()
    {
        ItemSlot[,] origin = slots;
        Initialize();
        return origin;
    }

    public int RemoveItem(System.Predicate<item> condition)
    {
        return default;
    }

    public int RemoveItem(item wantItem)
    {
        return default;
    }

    public int RemoveItem(item wantItem, int amount)
    {
        return default;
    }

    public int RemoveItemOnExistSlots(item wantItem, int amount)
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
    public void MoveItem(int startRow, int startColumn,Inventory targetInventory , int targetRow, int targetColumn, int amount = -1 )
    { 
        
    }

    public void UseItem(item target)
    {

    }

    public void UseItem(int startRow, int startColumn)
    { 
        
    }
}

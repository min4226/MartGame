using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
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

    public void HealPotionPlus()
    {
        item potion = DataManager.LoadDataFile<item>("HealPotion");
        AddItem(potion, 927);
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

    public ItemSlots[] GetAllSlot()
    {
        ItemSlots[] result = new ItemSlots[slots.Length];

        int height = slots.GetLength(0);
        int width = slots.GetLength(1);
        for (int row = 0; row < height; row++)
        {
            for (int column = 0; column < width; column++)
            {
                result[width * row + column] = slots[row, column];
            }
        }
        return result;
    }

    public ItemSlots FindItem(item target)
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
        slots[0, 0].AddItem(wantItem, amount);
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

    public ItemSlots[,] Clear()
    {
        ItemSlots[,] origin = slots;
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

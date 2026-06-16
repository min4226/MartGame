using UnityEngine;

public enum ItemType
{ 
    Equipment, Consumable, Material, Miscellaneous, Quest, Important, None, 
    Length
}

[CreateAssetMenu(fileName = "item", menuName = "Item/ItemBase")]
public class Item : idContainer
{
    [Header("ItemBaseInfo")]
    public int id;
    [Space]
    [Header("ItemDetail")]
    public ItemType type;
    public int maxStack;
    public float weight;

    public virtual int CompareByType(Item other)
    {
        if (other == null) return 1;
        int result = type - other.type;
        if (result != 0) return result;
        return id - other.id;
    }
}

using UnityEngine;

public enum ItemType
{ 
    Equipment, Consumable, Material, Miscellaneous, Quest, Important, None, 
    Length
}

[CreateAssetMenu(fileName = "item", menuName = "Item/ItemBase")]
public class item : idContainer
{
    public ItemType type;
    public int maxStack;
    public float weight;

    
}

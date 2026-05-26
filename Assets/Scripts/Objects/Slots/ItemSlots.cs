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
}

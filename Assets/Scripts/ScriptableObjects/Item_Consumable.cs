using UnityEngine;

[CreateAssetMenu(fileName = "Item_Consumable", menuName = "Item / Consumable")]
public class Item_Consumable : Item
{
    public virtual bool IsUsable(CharacterBase form, CharacterBase to) => true;
    public virtual void OnUse(CharacterBase from, CharacterBase to)
    {

    }

    public virtual bool IsUsable(CharacterBase from, Vector3 position) => true;
    public virtual void OnUse(CharacterBase from, Vector3 position)
    { 
        
    }
}

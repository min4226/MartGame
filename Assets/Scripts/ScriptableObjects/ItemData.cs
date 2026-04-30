using UnityEngine;

[CreateAssetMenu(fileName ="Item", menuName = "Scriptable Objects/ItemData")]
public class ItemData : ScriptableObject
{
    public Sprite itemSprite;
    public string itemName;
    public int itemPrice;

}

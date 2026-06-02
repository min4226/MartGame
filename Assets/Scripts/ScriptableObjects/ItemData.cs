using UnityEngine;

[CreateAssetMenu(fileName ="Item", menuName = "Scriptable Objects/ItemData")]
public class ItemData : ScriptableObject
{
    public GameObject itemSprite;
    public string itemName;
    public Reward reward;

}

using UnityEngine;

[CreateAssetMenu(fileName = "ExpulsionItem", menuName = "Scriptable Objects/ExpulsionItem")]
public class ExpulsionItem : ScriptableObject
{
    public Sprite ExpulsionItemSprite; 
    public string ExpulsionItemName; 
    public float ExpulsionDamage; 
}

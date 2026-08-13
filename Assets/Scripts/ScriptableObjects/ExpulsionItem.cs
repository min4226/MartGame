using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "ExpulsionItem", menuName = "Scriptable Objects/ExpulsionItem")]
public class ExpulsionItem : ScriptableObject
{
    public Sprite ExpulsionItemSprite; 
    public string ExpulsionItemName; 
    public int ExpulsionDamage;
    public UnityEvent OnRelease;
}

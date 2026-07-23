using UnityEngine;

[CreateAssetMenu(fileName = "ExpulsionItem", menuName = "Scriptable Objects/ExpulsionItem")]
public class ExpulsionItem : ScriptableObject
{
    public Sprite ExpulsionItemSprite; // 아이템 스프라이트
    public string ExpulsionItemName; // 아이템 이름
    public float ExpulsionDamage; // 아이템이 주는 데미지
}

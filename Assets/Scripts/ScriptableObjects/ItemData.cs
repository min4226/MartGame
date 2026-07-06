using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName ="Item", menuName = "Scriptable Objects/ItemData")]
public class ItemData : ScriptableObject
{
    public GameObject itemSprite; // 아이템 스프라이트
    public string itemName; // 아이템 이름
    public Reward reward; // 획득 보상
    public int itemBasePrice; // 아이템 기본 가격
}

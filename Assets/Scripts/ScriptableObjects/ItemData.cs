using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName ="Item", menuName = "Scriptable Objects/ItemData")]
public class ItemData : ScriptableObject
{
    public Sprite itemSprite; // 아이템 스프라이트
    public string itemName; // 아이템 이름
    public int itemBasePrice; // 아이템 기본 가격
    public GameObject itemPrefab; // instantiate에 사용할 게임오브젝트
}

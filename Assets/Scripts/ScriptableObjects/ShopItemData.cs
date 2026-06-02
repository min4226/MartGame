using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Shop", menuName = "Scriptable Objects/ShopItemData")]
public class ShopItemData : ScriptableObject
{
    public Sprite shopItemSprite;
    public string shopItemName;
    public Reward reward;

}

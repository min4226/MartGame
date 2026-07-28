using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName ="Item", menuName = "Scriptable Objects/ItemData")]
public class ItemData : ScriptableObject
{
    public Sprite itemSprite; // ������ ��������Ʈ
    public string itemName; // ������ �̸�
    public int itemBasePrice; // ������ �⺻ ����
    public GameObject itemPrefab; // instantiate�� ����� ���ӿ�����Ʈ
}
